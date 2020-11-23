$(document).ready(function () {
    $('#PhoneNumber').on('focusout', function () {
        var phoneNumber = $(this).val().trim();
        CheckExistedPhoneNumber(phoneNumber);
    });
});

function CheckExistedPhoneNumber(phoneNumber) {
    try {
        if (phoneNumber) {
            $.ajax({
                url: '/AdminArea/Account/CheckExist',
                type: 'POST',
                dataType: 'json',
                data: { phoneNumber: phoneNumber },
                success: function (response) {
                    if (response.isExisted) {
                        $('#check-existed').removeClass('text-navy');
                        $('#check-existed').addClass('text-danger');
                        $('#check-existed').text("Số điện thoại này đã được đăng kí!!!");
                    }
                    else {
                        $('#check-existed').removeClass('text-danger');
                        $('#check-existed').addClass('text-navy');
                        $('#check-existed').text("Bạn có thể sử dụng số điện thoại này!!!");
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    } catch (e) {
        console.log(e);
    }
}