const MOVIES = document.querySelectorAll(".js-movie");
const SEARCH_BTN = document.querySelector(".js-search");
let nameField = document.querySelector(".js-searching-name");
let dateField = document.querySelector(".js-searching-date");
let timeField = document.querySelector(".js-searching-time");

const DAY_TIME_DICT = {
    morning: ["06:00:00", "07:00:00", "08:00:00", "09:00:00", "10:00:00", "11:00:00"],
    day: ["12:00:00", "13:00:00", "14:00:00", "15:00:00", "16:00:00", "17:00:00"],
    evening: ["18:00:00", "19:00:00", "20:00:00", "21:00:00", "22:00:00", "23:00:00"],
    night: ["00:00:00", "01:00:00", "02:00:00", "03:00:00", "04:00:00", "05:00:00"]
};

function HideEl(el) {
    el.style.display = 'none';
}

function ShowEl(el) {
    el.style.display = 'block';
}

class Session {
    constructor(el) {
        this.element = el;
        this.date = el.querySelector(".js-session-date").textContent;
        this.time = el.querySelector(".js-session-time").textContent;
    }

    IsOnDate(date) {
        if (this.date === date) {
            return true;
        } else {
            return false;
        }
    }

    HideIfNotOnDateOrShow(date) {
        if (this.IsOnDate(date)) {
            ShowEl(this.element);
        } else {
            HideEl(this.element);
        }
    }

    IsOnTimeRange(range) {
        let flag = false;
        for (let i = 0; i < range.length; i++) {
            console.group();
            console.log(range[i]);
            console.log(this.time);
            console.groupEnd();
            if (range[i] === this.time) {
                flag = true;
            }
        }
        return flag;
    }

    HideIfNotOnTimeOrShow(range) {
        if (this.IsOnTimeRange(range)) {
            ShowEl(this.element);
        } else {
            HideEl(this.element);
        }
    }
}

class Movie {
    constructor(element) {
        this.element = element;
        this.title = element.querySelector(".card-title").textContent;
        this.sessions = [];
        element.querySelectorAll(".js-session").forEach((sessionEl) => {
            let session = new Session(sessionEl);
            this.sessions.push(session);
        })
    }

    HideIfNotContainsNameOrShow(searchingName) {
        if (!this.title.toLowerCase().includes(searchingName.toLowerCase())) {
            HideEl(this.element);
        } else {
            ShowEl(this.element);
        }
    }

    HideIfNoSuitableSessionsOrShowThemByDate(date) {
        for (let i = 0; i < this.sessions.length; i++) {
            if (this.sessions[i].IsOnDate(date)) {
                ShowEl(this.element);
                this.ShowSuitableSessionByDate(date);
                console.log("founded");
                return;
            }
        }
        HideEl(this.element);
    }

    HideIfNoSuitableSessionsOrShowThemByTime(timeRange) {
        for (let i = 0; i < this.sessions.length; i++) {
            if (this.sessions[i].IsOnTimeRange(timeRange)) {
                ShowEl(this.element);
                this.ShowSuitableSessionByTime(timeRange);
                console.log("founded");
                return;
            }
        }
        HideEl(this.element);
    }

    ShowSuitableSessionByDate(date) {
        this.sessions.forEach((session) => {
            session.HideIfNotOnDateOrShow(date);
        })
    }

    ShowSuitableSessionByTime(range) {
        this.sessions.forEach((session) => {
            session.HideIfNotOnTimeOrShow(range);
        })
    }
}

nameField.addEventListener("input", FindMoviesByName);

dateField.addEventListener("change", FindSessionsByDate);

timeField.addEventListener("change", FindSessionsByTime);

function FindMoviesByName(e) {
    let name = e.target.value;
    MOVIES.forEach((movie) => {
        let movieObject = new Movie(movie);
        movieObject.HideIfNotContainsNameOrShow(name);
    })
}

function FindSessionsByDate(e) {
    let searchingDate = e.target.value;

    MOVIES.forEach((movie) => {
        let movieObj = new Movie(movie);
        if (searchingDate !== "none") {
            movieObj.HideIfNoSuitableSessionsOrShowThemByDate(searchingDate);
        } else {
            ShowEl(movieObj.element);
        }
    });
}

function FindSessionsByTime(e) {
    let value = e.target.value;

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
