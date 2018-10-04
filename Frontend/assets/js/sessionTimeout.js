/**
 * Created by Spape on 8/05/2017.
 */
setTimeout(function () {
    sessionStorage.clear();
    window.location.href = "login.php";
}, 30 * 60000);

$('#afmelden').click(function () {
    sessionStorage.clear();
    window.location.href = "login.php";
});

if (sessionStorage.length === 0) {
    window.location.href = "login.php";
}
