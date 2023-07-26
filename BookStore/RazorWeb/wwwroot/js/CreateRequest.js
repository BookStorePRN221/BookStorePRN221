document.getElementById("registerBtn").addEventListener("click", function () {
    document.getElementById("registerModal").style.display = "block";
});

document.getElementById("addNewBookModal").style.display = "block";

document.getElementById("addNewBookBtn").addEventListener("click", function () {
    document.getElementById("addNewBookModal").style.display = "block";
    document.getElementById("addOldBookModal").style.display = "none";
});

document.getElementById("addOldBookBtn").addEventListener("click", function () {
    document.getElementById("addOldBookModal").style.display = "block";
    document.getElementById("addNewBookModal").style.display = "none";
});

document.getElementById("cancelBtn").addEventListener("click", function () {
    document.getElementById("registerModal").style.display = "none";
});

window.addEventListener("click", function (event) {
    if (event.target == document.getElementById("registerModal")) {
        document.getElementById("registerModal").style.display = "none";
    }
});
