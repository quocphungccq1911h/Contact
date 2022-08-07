var homeController = {
    initialize: function () {
        registerEvents();
    },
    registerEvents: function () {
        $('#frmSaveData').validate({
            rules: {
                txtName:"required"
            },
            messages: {
                txtName:"Ban phai nhap ten"
            }
        });
    }
}