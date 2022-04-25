$(document).ready(function () {

    $('#registrationBtn').click(function () {
        let registrationUrl = 'https://localhost:5001/api/user';

        let login = $('#loginForm').val();
        let password = $('#passwordForm').val();

        let credentials = {
            "login": login,
            "password": password
        }

        fetch(registrationUrl, {
            method: 'POST',
            body: JSON.stringify(credentials),
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(x => {
                if (!x.ok) {
                    x.text()
                        .then(res => {
                            console.log(res);                
                            $('#infoBlock')
                                .removeClass('d-none')
                                .addClass('alert-danger')
                                .text(JSON.parse(res).message);
                            ;
                        });
                } else {
                    x.json().then(result => {
                        let userInfo = parseJwt(result.data.token);
                        window.localStorage.setItem('gymapitoken', result.data.token);
                        $('#infoBlock')
                        .removeClass('d-none')
                        .addClass('alert-success')
                        .text(`Welcome, ${userInfo.unique_name}!`);
                        $('#formLoginIn').addClass('d-none');
                    });
                }
            });
    });
});