const MOVIES = document.querySelectorAll(".js-movie");
const SEARCH_BTN = document.querySelector(".js-search");
const FILTER_NAME = document.querySelector(".js-filter-name");

SEARCH_BTN.addEventListener("click", ()=>{
    FindMoviesByName(FILTER_NAME.value);
});

function FindMoviesByName(name){
    MOVIES.forEach((movie)=>{
        console.log(movie);
        if (!movie.querySelector(".card-title").textContent.toLowerCase().includes(name.toLowerCase())){
            movie.style.display = 'none';
        }
    })
}
