window.onload = function () {
    slideOne();
    slideTwo();
}

let sliderOne = document.getElementById("slider-1");
let sliderTwo = document.getElementById("slider-2");
let displayValOne = document.getElementById("range1");
let displayValTwo = document.getElementById("range2");
let minGap = 100;
let sliderTrack = document.querySelector(".slider-track");
let sliderMaxValue = document.getElementById("slider-1").max;
let sliderMinValue = document.getElementById("slider-1").min;

function slideOne() {
    if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
        sliderOne.value = parseInt(sliderTwo.value) - minGap;
    }
    displayValOne.textContent = sliderOne.value + " $";
    fillColor();
}
function slideTwo() {
    if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
        sliderTwo.value = parseInt(sliderOne.value) + minGap;
    }
    displayValTwo.textContent = sliderTwo.value + " $";
    fillColor();
}
function fillColor() {
    percent1 = ((sliderOne.value - sliderMinValue) / (sliderMaxValue - sliderMinValue)) * 100;
    percent2 = ((sliderTwo.value - sliderMinValue) / (sliderMaxValue - sliderMinValue)) * 100;
    percent2 = (sliderTwo.value / sliderMaxValue) * 100;
    sliderTrack.style.background = `linear-gradient(to right, #dadae5 ${percent1}% , #3264fe ${percent1}% , #3264fe ${percent2}%, #dadae5 ${percent2}%)`;
}


// the code of the tick box

function changeTickbox(elem) {
    if (elem.checked) {
        elem.nextElementSibling.style.backgroundColor = '#3264fe';
    } else {
        elem.nextElementSibling.style.backgroundColor = 'white';
    }
}


const clickableDiv = document.getElementsByClassName("clickableDiv");

for (let i = 0; i < clickableDiv.length; i++) {
    clickableDiv[i].addEventListener("click", () => {
        clickableDiv[i].classList.toggle("active");
    });
}

const collapseDivs = document.getElementsByClassName("clickable-div");

for (let i = 0; i < collapseDivs.length; i++) {
    collapseDivs[i].addEventListener("click", function () {
        collapseDivs[i].classList.toggle("active");
        let siblings = this.parentNode.children;
        for (let j = 0; j < siblings.length; j++) {
            let sibling = siblings[j];
            if (sibling.classList.contains("wd-scroll")) {
                if (sibling.style.display === "none") {
                    sibling.style.display = "block";
                } else {
                    sibling.style.display = "none";
                }
            }
        }
    });
}