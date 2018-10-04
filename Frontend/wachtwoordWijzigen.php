<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Wachtwoord wijzigen</title>

    <!-- Bootstrap & CSS -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:400,100,300,500">
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
                            <h3>Wachtwoord wijzigen</h3>
                            <p>Geef uw nieuw wachtwoord in om het te wijzigen.</p>
                        </div>
                        <div class="form-top-right">
                            <i class="fa fa-lock"></i>
                        </div>
                    </div>
                    <div class="form-bottom">
                        <form role="form" action="" method="post" class="login-form">
                            <div class="form-group">
                                <label class="sr-only" for="form-ww">Wachtwoord</label>
                                <input type="password" required name="form-username" placeholder="Wachtwoord..."
                                       class="form-username form-control" id="form-ww">
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="form-wwh">Wachtwoord herhalen</label>
                                <input type="password" required name="form-username"
                                       placeholder="Wachtwoord herhalen..." class="form-username form-control"
                                       id="form-wwh">
                            </div>
                            <button type="submit" class="btn">Wachtwoord wijzigen</button>
                            <br>
                            <br>
                            <p class="alert alert-success" id="sucess-label" hidden>Wachtwoord opgeslagen!</p>
                            <p class="alert alert-danger" id="error-label" hidden>Fout bij wijzigen wachtwoord!</p>
                            <p class="alert alert-info" id="info-label" hidden>Wachtwoord komt niet overeen!</p>
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
<script src="assets/js/apiConnection.js"></script>
<script type="text/javascript">
    $(document).ready(function () {

        $('.login-form').submit(function (e) {
            $('#info-label').hide();
            $('#sucess-label').hide();
            $('#error-label').hide();
            e.preventDefault();

            if (document.getElementById('form-wwh').value == document.getElementById('form-ww').value) {
                var data = {
                    "UserId": "<?php echo $_GET['userId']; ?>",
                    "Code": "<?php echo urlencode($_GET['code']); ?>",
                    "NewPassword": document.getElementById('form-wwh').value
                };
                $.ajax({
                    url: BaseURL+"/Account/ResetPassword",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(data),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
                    },
                    success: function () {
                        $("#sucess-label").show();
                    },
                    error: function () {
                        $("#error-label").show();
                    }
                });
            }
            else {
                $("#info-label").show();
            }
        });
    });
</script>

<!--[if lt IE 10]>
<script src="assets/js/placeholder.js"></script>
<![endif]-->

</body>
</html>