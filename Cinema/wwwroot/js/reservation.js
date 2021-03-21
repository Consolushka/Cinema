const ACTION_URL = `/Session/CreateReservation`;
const FORM = document.querySelector("#resForm");
let RESERVATION_BUTTON = document.querySelector("#reserv");
let availableSeats = FORM.querySelectorAll(".js-available-seat");
let reservationData;

availableSeats.forEach((seat) => {
    seat.addEventListener("click", () => {
        seat.querySelector("input").toggleAttribute("checked");
        seat.classList.toggle("seat--reserved");
    });
});

RESERVATION_BUTTON.addEventListener("click", (e) => {
    e.preventDefault();
    ReformatReservationData();
    console.log(reservationData);
    $.ajax({
        type: 'POST',
        url: ACTION_URL,
        data: JSON.stringify(reservationData),
        success: ShowSuccessPopup,
        error: function () {
            alert("Произошел сбой");
        }
    });
});

function ReformatReservationData() {
    let resultingData = {
        Session: Number(document.querySelector(".js-session").value),
        ReservedSeats: [],
        FName: '',
        SName: ''
    };
    FORM.querySelectorAll(".js-available-seat").forEach((seat) => {
        if (seat.classList.contains("seat--reserved")) {
            console.log(seat);
            resultingData.ReservedSeats.push(Number(seat.querySelector("input").value));
        }
    });
    resultingData.FName = FORM.querySelector("#FirstName").value;
    resultingData.SName = FORM.querySelector("#SecondName").value;
    reservationData = resultingData;
}

document.querySelectorAll(".js-popup-close").forEach((btn) => {
    btn.addEventListener("click", () => {
        btn.parentNode.classList.remove("popup--showed");
    })
})

function ShowSuccessPopup() {
    let successPopup = document.querySelector(".js-popup--success");
    successPopup.classList.add("popup--showed");
    successPopup.querySelector(".js-user-name").textContent = reservationData.FName + " " + reservationData.SName;
    successPopup.querySelector(".js-user-movie").textContent = document.querySelector(".js-movie").textContent;
    let dateTime = document.querySelector(".js-date").textContent;
    successPopup.querySelector(".js-user-date").textContent = dateTime.substring(0, dateTime.length - 8);
    successPopup.querySelector(".js-user-time").textContent = dateTime.substring(dateTime.length - 8, dateTime.length);
    reservationData.ReservedSeats.forEach((seat) => {
        successPopup.querySelector(".js-user-seats").textContent += seat + " ";
    });

}
