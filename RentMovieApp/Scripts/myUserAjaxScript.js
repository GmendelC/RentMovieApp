UserNameSpace = new {

    verificEmail : function (email) {
        return $.ajax({
            url: "User/IsEmptyEmail", dataType: "json",
            contentType: "json", contents: { email: email }
        });
    },

    emailInputChange : function (emailInput) {
        verificEmail(emailInput.value).done(function (isValid) {
            if (isValid)
                $(".email-validation").html = "";
            else
                $(".email-validation").html = "It email is register";
        });
    }
};