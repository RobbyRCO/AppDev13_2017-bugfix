<!DOCTYPE html>
<html lang="en" xmlns:http="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Login</title>

    <!-- Bootstrap & CSS -->
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/form-elements.css">
    <link rel="stylesheet" href="assets/css/style.css">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script>
        if (sessionStorage.length !== 0) {
            if(sessionStorage.getItem('role') === 'student'){
                window.location.href = "homescreenStudent.php";
            }else if(sessionStorage.getItem('role') === 'lector'){
                window.location.href = "homescreenLector.php";
            }else if(sessionStorage.getItem('role') === 'bedrijf'){
                window.location.href = "homescreenBedrijf.php";
            }else if(sessionStorage.getItem('role') === 'coordinator'){
                window.location.href = "homescreenStagecoordinator.php";
            }
        }
    </script>

    <link rel="apple-touch-icon" sizes="57x57" href="assets/ico/apple-icon-57x57.png">
    <link rel="apple-touch-icon" sizes="60x60" href="assets/ico/apple-icon-60x60.png">
    <link rel="apple-touch-icon" sizes="72x72" href="assets/ico/apple-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="76x76" href="assets/ico/apple-icon-76x76.png">
    <link rel="apple-touch-icon" sizes="114x114" href="assets/ico/apple-icon-114x114.png">
    <link rel="apple-touch-icon" sizes="120x120" href="assets/ico/apple-icon-120x120.png">
    <link rel="apple-touch-icon" sizes="144x144" href="assets/ico/apple-icon-144x144.png">
    <link rel="apple-touch-icon" sizes="152x152" href="assets/ico/apple-icon-152x152.png">
    <link rel="apple-touch-icon" sizes="180x180" href="assets/ico/apple-icon-180x180.png">
    <link rel="icon" type="image/png" sizes="192x192" href="assets/ico/android-icon-192x192.png">
    <link rel="icon" type="image/png" sizes="32x32" href="assets/ico/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="96x96" href="assets/ico/favicon-96x96.png">
    <link rel="icon" type="image/png" sizes="16x16" href="assets/ico/favicon-16x16.png">
    <link rel="manifest" href="assets/ico/manifest.json">
    <meta name="msapplication-TileColor" content="#ffffff">
    <meta name="msapplication-TileImage" content="assets/ico/ms-icon-144x144.png">
    <meta name="theme-color" content="#ffffff">
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
                            <h3>Inloggen</h3>
                            <p>Geef uw gebruikernaam en wachtwoord in om aan te melden:</p>
                        </div>
                        <div class="form-top-right">
                            <i class="fa fa-lock"></i>
                        </div>
                    </div>
                    <div class="form-bottom">
                        <div class="form-group">
                            <label class="sr-only" for="form-username">Gebruikersnaam</label>
                            <input type="text" name="form-username" placeholder="Gebruikernaam..."
                                   class="form-username form-control" id="form-username">
                        </div>
                        <div class="form-group">
                            <label class="sr-only" for="password">Wachtwoord</label>
                            <input type="password" name="form-password" placeholder="Wachtwoord..."
                                   class="form-password form-control" id="form-password">
                        </div>
                        <div class="login-form">
                            <button type="button" class="btn" id="login">
                                Aanmelden
                            </button>
                        </div>
                        <br>
                        <p class="alert alert-danger" id="error-label" hidden>ongeldige combinatie van paswoord en email!</p>
                        <form role="form" action="#" method="post" class="login-form" id="login-form">
                            <input type="hidden" id="username" name="username" value="#">
                            <input type="hidden" id="token" name="token" value="#">
                        </form>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3 social-login">
                    <div class="registreerbuttons">
                        <a class="btn btn-link-2" href="registreren.php">
                            Registreren
                        </a>
                        <a class="btn btn-link-2" href="registrerenBedrijf.php">
                            Registreren voor bedrijven
                        </a>
                        <a class="btn btn-link-2" href="wachtwoordVergeten.php">
                            Wachtwoord vergeten?
                        </a>
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
<script src="assets/js/dataTransfer.js"></script>
<script type="text/javascript">
    window.onload = assignEventlisteners();

    function assignEventlisteners() {
        document.getElementById("login").addEventListener("click",function () {
            login();
        });
        document.getElementById("form-password").addEventListener("keyup", function (event) {
            event.preventDefault();
            if (event.keyCode == 13){
                login();
            }
        });
        document.getElementById("form-username").addEventListener("keyup", function (event) {
            event.preventDefault();
            if (event.keyCode == 13){
                login();
            }
        })
    }

    function login(){
        authenticate(document.getElementById('form-username').value, document.getElementById('form-password').value);
    }
</script>
<script src="assets/js/apiConnection.js"></script>
<script type="text/javascript">
    function authenticate($username, $password) {
        $(document).ready(function () {
            $.ajax({
                url: BaseURL+'/api/Token',
                type: 'Post',
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                data: encodeURI('userName=' + $username + '&password=' + $password + '&grant_type=password&Submit=Login'),
                success: function (data) {
                    var $bearerToken = data.access_token;
                    var $username = data.userName;
                    var userId = data.userId;
                    var user = null;
                    $username = $username.substring(0, $username.indexOf('@'));
                    setHiddenField('username', $username);
                    setHiddenField('token', $bearerToken);
                    sessionStorage.setItem('tokenKey', $bearerToken);

                    if (data.emailConfirmed) {
                        if (data.isStudent) {
                            user = getUserAccountInfo(BaseURL+"/useraccountStudent/" + userId);
                            sessionStorage.setItem('role', 'student');
                            sessionStorage.setItem('userId', user.Id);
                            sessionStorage.setItem('username', user.Voornaam + " " + user.Achternaam);
                            setFormAction('login-form', "homescreenStudent.php");
                            submitForm('login-form');
                        }
                        if (data.isTeacher) {
                            user = getUserAccountInfo(BaseURL+"/useraccountLector/" + userId);
                            sessionStorage.setItem('role', 'lector');
                            sessionStorage.setItem('userId', user.Id);
                            sessionStorage.setItem('username', user.Voornaam + " " + user.Achternaam);
                            setFormAction('login-form', "homescreenLector.php");
                            submitForm('login-form');
                        }
                        if (data.isCompany) {
                            user = getUserAccountInfo(BaseURL+"/useraccountBedrijf/" + userId);
                            sessionStorage.setItem('role', 'bedrijf');
                            sessionStorage.setItem('userId', user.Id);
                            sessionStorage.setItem('username', user.Bedrijfsnaam);
                            setFormAction('login-form', "homescreenBedrijf.php");
                            submitForm('login-form');
                        }
                        if (data.isCoordinator) {
                            user = getUserAccountInfo(BaseURL+"/useraccountStagecoördinator/" + userId);
                            sessionStorage.setItem('role', 'coordinator');
                            sessionStorage.setItem('userId', user.Id);
                            sessionStorage.setItem('username', user.Voornaam + " " + user.Achternaam);
                            setFormAction('login-form', "homescreenStagecoordinator.php");
                            submitForm('login-form');
                        }
                    } else {
                        $("#error-label").show();
                    }
                },
                error: function () {
                    $("#error-label").show();
                }
            });

            function getUserAccountInfo(url) {
                var user = null;
                $.ajax({
                    url: url,
                    type: "Get",
                    dataType: "json",
                    async: false,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
                    },
                    success: function (data) {
                        user = data;
                    },
                    error: function (data, jqXHR, textStatus, errorThrown) {
                        console.error("Error: Status: " + textStatus + " Message: " + errorThrown + data);
                    }
                });
                return user;
            }
        });
    }
</script>

<!--[if lt IE 10]>
<script src="assets/js/placeholder.js"></script>
<![endif]-->

</body>

</html>