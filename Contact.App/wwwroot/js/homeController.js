var homeController = {
    init: function () {
        homeController.validationForm();
        homeController.registerEvents();
    },
    //submit form
    registerEvents: function () {
        $('form').submit(function (e) {
            if ($('#frmSaveData').valid()) {
                e.preventDefault();
                let phone = $('#txtPhone').val().trim();
                let name = $('#txtName').val().trim();
                let email = $('#txtEmail').val().trim();
                let content = $('#txtContent').val().trim();
                var url = $("#frmSaveData").attr("action");
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: {
                        name: name,
                        phone: phone,
                        email: email,
                        content: content,
                    },
                    success: function (res) {
                        if (res.isSuccessed == true) {
                            alert("Thêm liên hệ thành công");
                            window.location = '/'
                        } else if (res.messages != null) {
                            alert(res.messages);
                        } else {
                            alert("Không thành công");
                        }
                    },

                }).done(function (data) {
                    console.log(data);
                });
            }
        });
    },
    // validate form with ajax
    validationForm: function () {
        $('#frmSaveData').validate({
            rules: {
                Name: {
                    required: true
                },
                Email: {
                    required: true,
                    email: true,

                },
                Phone: {
                    required: true,
                    validationPhone: true,
                },
                Content: {
                    required: true,
                    minlength: 5
                }
            },
            messages: {
                Name: {
                    required: "Bạn phải nhập tên"
                },
                Email: {
                    required: "Bạn phải nhập email",
                    email: "Email không đúng định dạng"
                },
                Phone: {
                    required: "Bạn phải điện thoại",
                    validationPhone: "Số điện thoại không đúng"
                },
                Content: {
                    required: "Bạn phải nhập nội dung liên hệ",
                    minlength: "Vui lòng mô tả liên hệ ít nhất 5 ký tự"
                }
            }
        });
    }
}
homeController.init();
// validate phone number of vietnam
jQuery.validator.addMethod("validationPhone", function (phone_number, element) {
    phone_number = phone_number.replace(/\s+/g, "");
    return this.optional(element) || phone_number.length == 10 &&
        phone_number.match(/(0[3|5|7|8|9])+([0-9]{8})/g);
});