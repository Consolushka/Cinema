const MOVIES_ELEMENTS = document.querySelectorAll(".js-movie");
let moviesObj = [];
let nameField = document.querySelector(".js-searching-name");
let dateField = document.querySelector(".js-searching-date");
let timeField = document.querySelector(".js-searching-time");
let resetBtn = document.querySelector(".js-filters-reset");

const DAY_TIME_DICT = {
    morning: ["06:00:00", "07:00:00", "08:00:00", "09:00:00", "10:00:00", "11:00:00"],
    day: ["12:00:00", "13:00:00", "14:00:00", "15:00:00", "16:00:00", "17:00:00"],
    evening: ["18:00:00", "19:00:00", "20:00:00", "21:00:00", "22:00:00", "23:00:00"],
    night: ["00:00:00", "01:00:00", "02:00:00", "03:00:00", "04:00:00", "05:00:00"]
};

class Session {
    relevance = {
        onDate: true,
        onTime: true
    };

    constructor(el) {
        this.element = el;
        this.date = el.querySelector(".js-session-date").textContent;
        this.time = el.querySelector(".js-session-time").textContent;
    }

    DecideToShowOrHide(){
        // console.log(this.element);
        // console.log(this.relevance);
        if (this.relevance.onTime && this.relevance.onDate){
            this.element.parentElement.classList.add("show");
            ShowEl(this.element);
        }
        else {
            HideEl(this.element);
        }
    }

    IsFullyFit(){
        return this.relevance.onTime && this.relevance.onDate;
    }

    IsOnDate(date) {
        this.relevance.onDate =this.date === date;
        return this.relevance.onDate;
    }

    IsOnTimeRange(range) {
        let flag = false;
        for (let i = 0; i < range.length; i++) {
            if (range[i] === this.time) {
                flag = true;
                break;
            }
        }
        this.relevance.onTime = flag;
        return this.relevance.onTime;
    }
}

class Movie {
    relevance = {
        onTitle: true,
        onSessions: true
    }

    constructor(element) {
        this.element = element;
        this.title = element.querySelector(".card-title").textContent;
        this.sessions = [];
        element.querySelectorAll(".js-session").forEach((sessionEl) => {
            let session = new Session(sessionEl);
            this.sessions.push(session);
        })
    }

    DecideToShowOrHide(){
        let flag = false;
        this.sessions.forEach((session)=>{
            if (session.IsFullyFit()){
                flag = true;
            }
            session.DecideToShowOrHide();
        })
        this.relevance.onSessions = flag;
        if (this.relevance.onSessions || this.relevance.onTitle){
            ShowEl(this.element);
        }
        else{
            HideEl(this.element);
        }
    }

    HideIfNotContainsNameOrShow(searchingName) {
        this.relevance.onTitle = this.title.toLowerCase().includes(searchingName.toLowerCase());
        this.DecideToShowOrHide();
    }

    HideIfNoSuitableSessionsOrShowThemByDate(date) {
        for (let i = 0; i < this.sessions.length; i++) {
            if (this.sessions[i].IsOnDate(date)) {
                ShowEl(this.element);
            }
        }
        this.DecideToShowOrHide();
    }

    HideIfNoSuitableSessionsOrShowThemByTime(timeRange) {
        for (let i = 0; i < this.sessions.length; i++) {
            if (this.sessions[i].IsOnTimeRange(timeRange)) {
                ShowEl(this.element);
            }
        }
        this.DecideToShowOrHide();
    }
}

nameField.addEventListener("input", FindMoviesByName);

dateField.addEventListener("change", FindSessionsByDate);

timeField.addEventListener("change", FindSessionsByTime);

resetBtn.addEventListener("click", ResetFilters);

MOVIES_ELEMENTS.forEach((movieEl)=>{
    let movieObj = new Movie(movieEl);
    moviesObj.push(movieObj);
})

function HideEl(el) {
    el.style.display = 'none';
}

function ShowEl(el) {
    el.style.display = 'block';
}

function FindMoviesByName(e) {
    let value;
    if (e === undefined){
        value = "";
    }
    else {
        value = e.target.value;
    }
    moviesObj.forEach((movie) => {
        movie.HideIfNotContainsNameOrShow(value);
    })
}

function FindSessionsByDate(e) {
    let value;
    if (e === undefined){
        value = "none";
    }
    else {
        value = e.target.value;
    }

    moviesObj.forEach((movie) => {
        if (value !== "none") {
            movie.HideIfNoSuitableSessionsOrShowThemByDate(value);
        } else {
            ShowEl(movie.element);
        }
    });
}

function FindSessionsByTime(e) {
    let value;
    if (e === undefined){
        value = "none";
    }
    else {
        value = e.target.value;
    }


    moviesObj.forEach((movie) => {
        if (value !== "none") {
            let searchingDayTime = DAY_TIME_DICT[value];
            movie.HideIfNoSuitableSessionsOrShowThemByTime(searchingDayTime);
        } else {
            movie.sessions.forEach((session) => {
                ShowEl(session.element);
            })
        }
    });
}

function ResetFilters(){
    FindMoviesByName();
    FindSessionsByTime();
    FindSessionsByDate();
}
