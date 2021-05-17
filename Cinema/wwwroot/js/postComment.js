const COMM_TEMPLATE = document.querySelector(`#commTempl`).content.querySelector(`.comment`);
const ACTION_URL = `/Movies/AddComment`;
document.querySelector(".movie-comment__new-btn").addEventListener("click", (e) => {
    e.preventDefault();
    let comment = ReformatCommentData();
    console.log(comment);
    $.ajax({
       type: 'POST',
       url: ACTION_URL,
       data: JSON.stringify(comment),
       success: ()=> {
           SuccessfullAdding(comment)
       },
       error: function () {
           alert("Произошел сбой");
       }
    });
});

function ReformatCommentData() {
    let resData = {
        MovieId: Number(document.querySelector(".movie-comment__new-id").value),
        PersonName: document.querySelector(".movie-comment__new-person").value,
        Text: document.querySelector(".movie-comment__new-text").value
    };
    return resData;
}

function SuccessfullAdding(comment){
    let commentFragment = COMM_TEMPLATE.cloneNode(true);
    commentFragment.querySelector(".comment-name").textContent = comment.PersonName;
    commentFragment.querySelector(".comment-text").textContent = comment.Text;
    document.querySelector(".movie-comment__list").insertAdjacentHTML(`beforeend`, commentFragment.outerHTML);
}