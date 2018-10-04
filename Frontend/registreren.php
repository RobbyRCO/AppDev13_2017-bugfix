<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Registreren</title>

    <!-- CSS -->
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/form-elements.css">
    <link rel="stylesheet" href="assets/css/style.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="assets/ico/favicon.png">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="assets/ico/apple-touch-icon-57-precomposed.png">
    <script type="text/javascript" src="//platform.linkedin.com/in.js">
        api_key:   78f7aqrbspv8ex
        authorize: true
        onLoad:    onLinkedInLoad
        lang:      nl_NL
        scope:     r_basicprofile
        r_emailaddress
    </script>
    <script type="text/javascript">

        // Setup an event listener to make an API call once auth is complete
        function onLinkedInLoad() {
            IN.Event.on(IN, "auth", getProfileData);
        }

        // Handle the successful return from the API call
        function onSuccess(data) {
            var jsonData = JSON.parse(JSON.stringify(data.values[0]));
            document.getElementById("form-voornaam").value = jsonData.firstName;
            document.getElementById("form-naam").value = jsonData.lastName;
            document.getElementById("form-cemail").value = jsonData.emailAddress;
            IN.User.logout();
        }

        // Handle an error response from the API call
        function onError(error) {
            alert(error.message);
            console.log(error);
            IN.User.logout();
        }

        // Use the API call wrapper to request the member's basic profile data
        function getProfileData() {
            IN.API.Profile("me").fields("first-name", "last-name", "email-address").result(onSuccess).error(onError);
        }
    </script>
</head>

<body>
<!-- Top content -->
<div class="top-content">
    <div class="inner-bg">
        <div class="container">
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3 form-box">
                    <div class="form-top">
                        <div class="form-top-left strokeme">
                            <h3>Registreren</h3>
                            <script type="in/Login"></script>
                            <p>Vul de gevraagde gegevens in om een account aan te maken. Of meldt u aan met LinkedIn om
                                dit formulier automatisch aan te vullen.</p>
                        </div>
                        <div class="form-top-right">
                            <i class="fa fa-lock"></i>
                        </div>
                    </div>
                    <div class="form-bottom">
                        <form id="form-registereren" role="form" action="" method="post" class="login-form">
                            <div class="form-group">
                                <label class="sr-only" for="form-email">School e-mailadres</label>
                                <input type="email" required name="form-email" placeholder="School e-mailadres..."
                                       class="form-email form-control" id="form-email">
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-cemail">Persoonlijk e-mailadres</label>
                                <input type="email" required name="form-cemail" placeholder="Persoonlijk e-mailadres..."
                                       class="form-cemail form-control" id="form-cemail">
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-naam">Naam</label>
                                <input type="text" required name="form-naam" placeholder="Naam..."
                                       class="form-naam form-control" id="form-naam">
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-voornaam">Voornaam</label>
                                <input type="text" required name="form-voornaam" placeholder="Voornaam..."
                                       class="form-voornaam form-control" id="form-voornaam">
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-studentennummer">Studentennummer</label>
                                <input type="number" required name="form-studentennummer"
                                       placeholder="Studentennummer..." class="form-studentennummer form-control"
                                       id="form-studentennummer">
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-ww">Wachtwoord</label>
                                <input type="password" minlength="6" pattern=".{3,}" required name="form-ww"
                                       placeholder="Wachtwoord..."
                                       class="form-control" id="form-ww">
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-wwh">Wachtwoord herhalen</label>
                                <input type="password" minlength="6" pattern=".{3,}" required name="form-wwh"
                                       placeholder="Wachtwoord herhalen..."
                                       class="form-control" id="form-wwh">
                            </div>
                            <button type="submit" class="btn">Verzenden</button>
                            <br/>
                            <p class="alert alert-success" id="sucess-label" hidden>Gelukt! U zal spoedig een mail
                                ontvangen.</p>
                            <p class="alert alert-danger" id="error-label" hidden>Er is iets misgegaan</p>
                            <p class="alert alert-info" id="info-label" hidden>Wachtwoorden moeten hetzelfde zijn en
                                minimaal 6 karakters lang.</p>
                        </form>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3 social-login">
                        <div class="registreerbuttons">
                            <a class="btn btn-link-2" href="login.php">
                                Terug naar inloggen
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Javascript -->
<script src="assets/js/jquery-1.11.1.min.js"></script>
<script src="assets/bootstrap/js/bootstrap.min.js"></script>
<script src="assets/js/jquery.backstretch.min.js"></script>
<script src="assets/js/scripts.js"></script>
<script src="assets/js/placeholder.js"></script>
<script src="assets/js/apiConnection.js"></script>

<script>

    $(document).ready(function () {

        var userId = "";
        var userRole = "";
        var urlLectorOfStudent = "";

        $('#form-registereren').submit(function (e) {
            e.preventDefault();

            if ($('#form-ww').val().length < 6 || $('#form-wwh').val() != ($('#form-ww').val())) {
                $('#info-label').show();
            }


            if (!controleSchoolMail()) {
                alert("Gelieve een PXL email adres in te voeren");
                throw new Error("Wrong type of mail");

            }
            urlLectorOfStudent = controleStudentOfLector();
            userAccountPost();
            e.preventDefault();
        });


        function userAccountPost() {
            var userData = {
                UserName: $('#form-email').val(),
                Email: $('#form-email').val(),
                Password: $('#form-ww').val(),
                ConfirmPassword: $('#form-wwh').val(),
                UserRole: userRole
            };

            $.ajax({
                type: 'POST',
                async: false,
                url: BaseURL+'/Account/Register',
                contentType: "application/json",
                data: JSON.stringify(userData),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
                },
                success: function (data) {
                    userId = data.Id;
                    schoolPersoonPost();
                    alert("U zal spoedig een mail ontvangen");

                },
                error: function () {
                    $("#error-label").show();
                }
            });
        }

        function schoolPersoonPost() {
            var schoolpersoonData = {

                SchoolMail: $('#form-email').val(),
                Email: $('#form-cemail').val(),
                Achternaam: $('#form-naam').val(),
                Voornaam: $('#form-voornaam').val(),
                Nummer: $('#form-studentennummer').val(),
                UserAccountId: userId
            };

            $.ajax({
                type: 'POST',
                async: false,
                url: urlLectorOfStudent,
                contentType: "application/json",
                data: JSON.stringify(schoolpersoonData),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
                },
                success: function () {
                    $('#sucess-label').show();
                },
                error: function () {
                    $("#error-label").show();
                }
            });
        }


        function controleSchoolMail() {

            var mail = $('#form-email').val().split("@");

            if (mail[1].toLowerCase() == ("pxl.be") || mail[1].toLowerCase() == ("student.pxl.be")) {
                return true;
            }
            else {
                return false;
            }
            return false;
        }

        function controleStudentOfLector() {
            var mail = $('#form-email').val();
            var einde = mail.split("@");
            if (einde[1].toLowerCase() == ("student.pxl.be")) {
                userRole = "Student";
                return BaseURL+'/student';

            }
            userRole = "Teacher";
            return BaseURL+'/lector';


        }
    });
</script>

</body>
</html>