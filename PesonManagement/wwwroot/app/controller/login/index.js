var LoginController = function () {
    this.initialize = function () {
        registerEvents();
    };
    var login = function (user, pass) {
        $.ajax({
            type: 'POST',
            data: {
                UserName: user,
                Password: pass
            },
            dateType: 'json',
            url: '/admin/login/authen',
            success: function (res) {
                if (res.Success) {
                    util.notify('Login success', 'success');
                    var urlRedirect = util.getParamUrl('ReturnUrl');
                    // debugger;
                    window.location.href = util.getParamUrl('ReturnUrl') || '/Admin/Home/Index';

                } else {
                    util.notify('Login failed', 'error');
                }
            }
        });
    };
    var registerEvents = function () {
        $('#btnLogin').off('click').on('click', function () {
            var user = $('#txtUserName').val();
            var password = $('#txtPassword').val();
            login(user, password);
        });
    }
};
