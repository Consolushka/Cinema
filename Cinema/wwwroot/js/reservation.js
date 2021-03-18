const ID = window.location.href.split('?')[0].split('/').filter(function (i) { return i !== "" }).slice(-1)[0];
const ACTION = `/Session/CreateReservation`;
const FORM = document.querySelector("#resForm");
let resBtn = document.querySelector("#reserv");
let availableSeats = FORM.querySelectorAll(".js-available-seat");

console.log(ACTION);

availableSeats.forEach((seat)=>{
    seat.addEventListener("click", ()=>{
        seat.toggleAttribute("checked");
    })
})

resBtn.addEventListener("click", (e)=>{
    e.preventDefault();
    //let data = JSON.stringify(GetFormData());
    //console.log(data);
    $.ajax({
        type: 'POST',
        url: ACTION,
        data: JSON.stringify(GetFormData()),
        success: function (data) {
            console.log(data)
        },
        error: function () {
            alert("Произошел сбой");
        }
    });
});



function GetFormData(){
    let resultingData = {
        Session: Number(ID),
        ReservedSeats: [],
        FName: '',
        SName: ''
    };
    FORM.querySelectorAll(".js-available-seat").forEach((seat)=>{
        if (seat.hasAttribute("checked")){
            console.log(seat);
            resultingData.ReservedSeats.push(Number(seat.dataset.seatnumber));
        }
    });
    resultingData.FName = FORM.querySelector("#FirstName").value;
    resultingData.SName = FORM.querySelector("#SecondName").value;
    return resultingData;
}
