<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Persoonlijke gegevens</title>

    <!-- Bootstrap & CSS -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/css/styleHome.css" rel="stylesheet">
    <link href="assets/css/datatables.min.css" rel="stylesheet">

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
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <a id="afmelden" class="btn btn-link">Afmelden<img
                        id="afmeldenPng"
                        alt="afmelden"
                        src="assets/img/afmelden.png"/></a>
        </div>
    </div>
</div>
<nav class="navbar navbar-inverse navbar-static-top" role="navigation">
    <div class="container">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse"
                    data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a href="homescreenBedrijf.php" class="hidden-sm hidden-xs logo"><img src="assets/img/pxllogowitterand.png"></a>
        </div>

        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li><a href="homescreenBedrijf.php">Home</a></li>
                <li><a href="indienenstagevoorstelBedrijf.php">Stagevoorstel indienen</a></li>
                <li class="current"><a href="GegevensBijwerkenBedrijf.php">Persoonlijke gegevens</a></li>
            </ul>
        </div>
    </div>
</nav>
<div class="container">
    <h2>
        Mijn profiel bewerken
    </h2>
    <span style="color: red">Vereist *</span>
    <form id="gegevensForm">
        <div id="gegevens">
            <h3>Persoonlijke gegevens</h3>
            <br>
            <div class="form-group">
                <label for="naam">Bedrijfsnaam *</label>
                <input type="text" name="Naam" id="naam" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="adres">Adres *</label>
                <input type="text" name="Adres" id="adres" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="huisnummer">Huisnummer *</label>
                <input type="text" name="Huisnummer" id="huisnummer" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="busnummer">Busnummer</label>
                <input type="text" name="Busnummer" id="busnummer" class="form-control">
            </div>
            <div class="form-group">
                <label for="postcode">Postcode *</label>
                <input type="text" name="Postcode" id="postcode" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="gemeente">Gemeente *</label>
                <input type="text" name="Gemeente" id="gemeente" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="telefoonnummer">Telefoonnummer *</label>
                <input type="text" name="Telefoonnummer" id="telefoonnummer" class="form-control" required
                       pattern=".*\+?\(?\d{2,4}\)?[\d\s-]{3,}.*">
            </div>
            <div class="form-group">
                <label for="email">E-mail *</label>
                <input type="email" name="Email" id="email" class="form-control" required
                       pattern=".*[a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+.*">
            </div>
        </div>
        <br>
        <button type="submit" value="Bewerken" class="btn btn-indienen" id="sendButton">Bewerken</button>
        <br>
        <br>
    </form>
</div>


<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script src="assets/js/jquery.min.js"></script>
<script src="assets/js/bootstrap.min.js"></script>
<script src="assets/js/sessionTimeout.js"></script>
<script src="assets/js/apiConnection.js"></script>
<script>
    var bedrijf;
    $(document).ready(function () {
        $.ajax({
            url: BaseURL+"/bedrijf/" + sessionStorage.getItem('userId'),
            type: "Get",
            dataType: "json",
            async: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
            },
            success: function (data) {
                bedrijf = data;
                document.getElementById("naam").value = data.Bedrijfsnaam;
                document.getElementById("adres").value = data.Adres;
                document.getElementById("postcode").value = data.Postcode;
                document.getElementById("gemeente").value = data.Gemeente;
                document.getElementById("huisnummer").value = data.Huisnummer;
                document.getElementById("busnummer").value = data.Busnummer;
                document.getElementById("telefoonnummer").value = data.Telefoonnummer;
                document.getElementById("email").value = data.Email;
            },
            error: function (data, jqXHR, textStatus, errorThrown) {
                console.error("Error: Status: " + textStatus + " Message: " + errorThrown + data);
            }
        });

        $("#gegevensForm").submit(function () {
            bedrijf.Bedrijfsnaam = $('#naam').val();
            bedrijf.Adres = $('#adres').val();
            bedrijf.Postcode = $('#postcode').val();
            bedrijf.Gemeente = $('#gemeente').val();
            bedrijf.Huisnummer = $('#huisnummer').val();
            bedrijf.Busnummer = $('#busnummer').val();
            bedrijf.Telefoonnummer = $('#telefoonnummer').val();
            bedrijf.Email = $('#email').val();
            $.ajax({
                url: BaseURL+"/bedrijf/" + sessionStorage.getItem('userId'),
                type: "PUT",
                data: bedrijf,
                async: false,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
                },
                error: function (data, jqXHR, textStatus, errorThrown) {
                    console.error("Error: Status: " + textStatus + " Message: " + errorThrown + data);
                }
            });
        });
    });
</script>
</body>
</html>