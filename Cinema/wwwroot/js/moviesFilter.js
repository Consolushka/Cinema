const MOVIES = document.querySelectorAll(".js-movie");
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

    IsOnDate(date) {
        this.relevance.onDate =this.date === date;
        return this.relevance.onDate;
    }

    HideIfNotOnDateOrShow() {
        if (this.relevance.onDate) {
            ShowEl(this.element);
        } else {
            HideEl(this.element);
        }
    }

    IsOnTimeRange(range) {
        let flag = false;
        for (let i = 0; i < range.length; i++) {
            if (range[i] === this.time) {
                flag = true;
            }
        }
        this.relevance.onTime = flag;
        return this.relevance.onTime;
    }

    HideIfNotOnTimeOrShow() {
        if (this.relevance.onTime) {
            ShowEl(this.element);
        } else {
            HideEl(this.element);
        }
    }
}

class Movie {
    relevance = {
        onTitle: true,
        onSessions: {
            onDate: true,
            onTime: true
        }
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
        if (this.relevance.onTitle && this.relevance.onSessions.onDate && this.relevance.onSessions.onTime){
            ShowEl(this.element);
        }
        else {
            HideEl(this.element);
        }
        console.log(this.element);
        console.log(this.relevance);
    }

    HideIfNotContainsNameOrShow(searchingName) {
        this.relevance.onTitle = this.title.toLowerCase().includes(searchingName.toLowerCase());
        this.DecideToShowOrHide();
    }

    HideIfNoSuitableSessionsOrShowThemByDate(date) {
        let flag = false;
        for (let i = 0; i < this.sessions.length; i++) {
            if (this.sessions[i].IsOnDate(date)) {
                flag = true;
                ShowEl(this.element);
            }
        }
        this.relevance.onSessions.onDate = flag;
        this.ShowSuitableSessionByDate();
        this.DecideToShowOrHide();
    }

    HideIfNoSuitableSessionsOrShowThemByTime(timeRange) {
        let flag = false;
        for (let i = 0; i < this.sessions.length; i++) {
            if (this.sessions[i].IsOnTimeRange(timeRange)) {
                flag = true;
                ShowEl(this.element);
            }
        }
        this.relevance.onSessions.onTime = flag;
        this.ShowSuitableSessionByTime();
        this.DecideToShowOrHide();
        // if (this.relevance.onSessions){
        //     this.ShowSuitableSessionByDate();
        // }
        // else{
        //
        //     HideEl(this.element);
        // }
    }

    ShowSuitableSessionByDate() {
        this.sessions.forEach((session) => {
            session.HideIfNotOnDateOrShow();
        })
    }

    ShowSuitableSessionByTime() {
        this.sessions.forEach((session) => {
            session.HideIfNotOnTimeOrShow();
        })
    }
}

nameField.addEventListener("input", FindMoviesByName);

dateField.addEventListener("change", FindSessionsByDate);

timeField.addEventListener("change", FindSessionsByTime);

resetBtn.addEventListener("click", ResetFilters);

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
    MOVIES.forEach((movie) => {
        let movieObject = new Movie(movie);
        movieObject.HideIfNotContainsNameOrShow(value);
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

    MOVIES.forEach((movie) => {
        let movieObj = new Movie(movie);
        if (value !== "none") {
            movieObj.HideIfNoSuitableSessionsOrShowThemByDate(value);
        } else {
            ShowEl(movieObj.element);
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


    MOVIES.forEach((movie) => {
        let movieObj = new Movie(movie);
        if (value !== "none") {
            let searchingDayTime = DAY_TIME_DICT[value];
            movieObj.HideIfNoSuitableSessionsOrShowThemByTime(searchingDayTime);
        } else {
            movieObj.sessions.forEach((session) => {
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
