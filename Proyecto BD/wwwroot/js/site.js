// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function EnableModify(name) {
    console.log("Test");
    inputs = document.getElementsByClassName("a" + name + "-input");
    console.log("a" + name + "-input")
    console.log(inputs.length)
    if (inputs[0].disabled) {
        Array.from(inputs).forEach(e => {
            e.disabled = false;
            console.log(e.name);
        }
        );
        document.getElementsByClassName("a" + name + "-mod")[0].style.display = "none";
        document.getElementsByClassName("a" + name + "-apply")[0].style.display = "block";
        console.log("Test2");
    }
    else {
        Array.from(inputs).forEach(e => {
            e.disabled = true;
            console.log(e.name);
        }
        );
        console.log("Test2");
    }
    
}