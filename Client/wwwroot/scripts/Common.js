//document.querySelector(".open-modal-button").addEventListener('click', function () {
//    document.querySelector("#modal-container").style.display = 'block';
//    document.querySelector("body").style.overflow = 'hidden';
//});

///* when modal is closed */
//document.querySelector("#close-modal-button").addEventListener('click', function () {
//    document.querySelector("#modal-container").style.display = 'none';
//    document.querySelector("body").style.overflow = 'visible';
//});


function HideMainScroll() {
//document.querySelector("#modal-container").style.display = 'block';
//document.querySelector("body").style.overflow = 'hidden';
    const disID = document.getElementById("modalContainer");
    disID.style.display = 'block';
    document.querySelector("body").style.overflow = 'hidden';
}