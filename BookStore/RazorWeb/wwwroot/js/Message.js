    function closeNotification() {
        var notification = document.getElementById("success-notification");
    notification.style.opacity = "0";
    setTimeout(function() {
        notification.style.display = "none";
        }, 500);
    }

    setTimeout(function() {
        closeNotification();
    }, 3000);
