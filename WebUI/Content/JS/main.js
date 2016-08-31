window.addEventListener('load', load, false);

function load() {
    var link = document.querySelector(".mapBlock .btnPrimary"),
        textInput = textInput = document.querySelectorAll('.inputText'),
        len = textInput.length,
        placeholder = '',
        i = 0,
        storage = localStorage.getItem("name");

    if (link) {
        var popup = document.querySelector(".popupForm"),
            form = document.querySelector('.popupFeedbackForm'),
            close = form.querySelector(".closeIco"),
            email = form.querySelector("#emailInput"),
            name = form.querySelector("#nameInput"),
            backPopup = document.querySelector(".backPopup");

        link.addEventListener('click', openPopup);
        close.addEventListener('click', closePopup);
        form.addEventListener('submit', submitForm);
        window.addEventListener('keydown', escapePopup);
    }

    for (i = 0; i < len; i += 1) {
        textInput[i].addEventListener('focus', checkFocus);
        textInput[i].addEventListener('blur', setPlaceholder);
    }

    function openPopup(event) {
        event.preventDefault();
        popup.classList.remove("none");
        backPopup.classList.remove("none");

        if (storage) {
            name.value = storage;
            name.nextElementSibling.classList.remove('none');
            email.focus();
            email.nextElementSibling.classList.remove('none');
        } else {
            name.focus();
        }
    }

    function closePopup(event) {
        event.preventDefault();
        popup.classList.remove("choice");
        popup.classList.remove("popupError");
        backPopup.classList.remove("choice");
        popup.classList.add("none");
        backPopup.classList.add("none");
    }

    function submitForm(event) {
        if (!name.value || !email.value) {
            event.preventDefault();
            popup.classList.remove("popupError");
            popup.offsetWidth = popup.offsetWidth;
            popup.classList.add("popupError");
        } else {
            localStorage.setItem("name", name.value);
        }
    }

    function escapePopup(event) {
        if (event.keyCode === 27) {
            if (!(popup.classList.contains('none'))) {
                popup.classList.remove("popupError");
                popup.classList.add("none");
                backPopup.classList.add("none");
            }
        }
    }

    function checkFocus(event) {
        target = event.target;
        placeholder = target.placeholder;
        target.placeholder = '';
        if (target.value === '') {
            var fakePlaceHolder = target.nextElementSibling;
            fakePlaceHolder.classList.remove('none');
        }
    }

    function setPlaceholder(event) {
        target = event.target;
        target.placeholder = placeholder;
        if (target.value === '') {
            var fakePlaceHolder = target.nextElementSibling;
            fakePlaceHolder.classList.add('none');
        }
    }
}
