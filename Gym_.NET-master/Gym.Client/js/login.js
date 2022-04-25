function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};

$(document).ready(function () {

    $('#loginBtn').click(function () {
        let loginUrl = 'https://192.168.0.177:5001/api/auth/authenticate';

        let login = $('#loginForm').val();
        let password = $('#passwordForm').val();

        let credentials = {
            "login": login,
            "password": password
        }

        fetch(loginUrl, {
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