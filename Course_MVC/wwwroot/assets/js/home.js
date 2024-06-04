"use strict";

let dropdown = document.querySelector(".home");
let openedList = document.querySelector(".open-down");

let categoryDropdown = document.querySelector(".category");
let openedCategories = document.querySelector(".category-dropdown")

let isDropdownOpen = false;
let isCategoryDropdownOpen = false;

function openDropdown() {
    openedList.classList.add("active-dropdown");
    isDropdownOpen = true;
}

function closeDropdown() {
    if (!isDropdownOpen) {
        openedList.classList.remove("active-dropdown");
    }
}

function openCategoryDropdown() {
    openedCategories.classList.add("active-category");
    isCategoryDropdownOpen = true;
}

function closeCategoryDropdown() {
    if (!isCategoryDropdownOpen) {
        openedCategories.classList.remove("active-category");
    }
}

dropdown.addEventListener("mouseover", openDropdown);
dropdown.addEventListener("mouseout", closeDropdown);
openedList.addEventListener("mouseover", function() {
    isDropdownOpen = true;
});
openedList.addEventListener("mouseout", function() {
    isDropdownOpen = false;
});

categoryDropdown.addEventListener("mouseover", openCategoryDropdown);
categoryDropdown.addEventListener("mouseout", closeCategoryDropdown);
openedCategories.addEventListener("mouseover", function() {
    isCategoryDropdownOpen = true;
});
openedCategories.addEventListener("mouseout", function() {
    isCategoryDropdownOpen = false;
});

document.addEventListener("mouseover", function(event) {
    if (!isDropdownOpen && !isCategoryDropdownOpen) {
        closeDropdown();
        closeCategoryDropdown();
    }
});

let arrow = document.querySelector(".visible");
let disappearArrow = document.querySelector(".disappear")

arrow.addEventListener("mouseover", function () {
    disappearArrow.classList.remove("disappear");
    disappearArrow.classList.add("visible");
    this.classList.remove("visible");
    this.classList.add("disappear");
    arrow.removeEventListener("mouseover", mouseOverHandler);
})
arrow.addEventListener("mouseout", function () {
    disappearArrow.classList.add("disappear");
    disappearArrow.classList.remove("visible");
    this.classList.add("visible");
    this.classList.remove("disappear");
})

const navbar = document.querySelector(".navbar");
let startPosition = navbar.offsetTop;
function stickynavbar() {
  if (window.scrollY > 90 +startPosition) {    
    navbar.classList.add('fixed-top');
  } else {
    navbar.classList.remove('fixed-top');    
  }
}
window.addEventListener('scroll', stickynavbar);


let allBtn = document.querySelector(".all");
let popularBtn = document.querySelector(".popular");
let trendBtn = document.querySelector(".trend");
let featuredBtn = document.querySelector(".featured");
let artBtn = document.querySelector(".art");
let tabList = document.querySelectorAll(".tab-list li");

let courses = document.querySelectorAll(".card");

allBtn.addEventListener("click",function(){
    tabList.forEach(li => {
        li.style.color = "#0e1133";
    });
    this.style.color = "#2b4eff";
    courses.forEach(course => {
        course.classList.remove("d-none");
    });
    
})

popularBtn.addEventListener("click",function(){
    tabList.forEach(li => {
        li.style.color = "#0e1133";
    });
    this.style.color = "#2b4eff";
    courses.forEach(course => {
        course.classList.add("d-none");
        if(course.classList.contains("popular-card")){
            course.classList.remove("d-none");
        }
    });
})

trendBtn.addEventListener("click",function(){
    tabList.forEach(li => {
        li.style.color = "#0e1133";
    });
    this.style.color = "#2b4eff";
    courses.forEach(course => {
        course.classList.add("d-none");
        if(course.classList.contains("trending-card")){
            course.classList.remove("d-none");
        }
    });
})

featuredBtn.addEventListener("click",function(){
    tabList.forEach(li => {
        li.style.color = "#0e1133";
    });
    this.style.color = "#2b4eff";
    courses.forEach(course => {
        course.classList.add("d-none");
        if(course.classList.contains("featured-card")){
            course.classList.remove("d-none");
        }
    });
})

artBtn.addEventListener("click",function(){
    tabList.forEach(li => {
        li.style.color = "#0e1133";
    });
    this.style.color = "#2b4eff";
    courses.forEach(course => {
        course.classList.add("d-none");
        if(course.classList.contains("art-card")){
            course.classList.remove("d-none");
        }
    });
})

let monthlyBtn = document.querySelector(".monthly");
let annualBtn = document.querySelector(".annual");

monthlyBtn.addEventListener("click",function(){
    this.classList.add("btn-primary");
    this.classList.remove("btn-light")
    annualBtn.classList.remove("btn-primary");
    annualBtn.classList.add("btn-light")
})

annualBtn.addEventListener("click",function(){
    this.classList.add("btn-primary");
    this.classList.remove("btn-light")
    monthlyBtn.classList.remove("btn-primary");
    monthlyBtn.classList.add("btn-light")
})

let sidebarIcon = document.querySelector(".fa-bars");
let sidebar = document.querySelector(".side-bar");
let closeIcon = document.querySelector(".btn-close");

sidebarIcon.addEventListener("click",function(){
    sidebar.classList.remove("d-none");
    this.classList.add("d-none");
})

closeIcon.addEventListener("click",function(){
    sidebar.classList.add("d-none");
    sidebarIcon.classList.remove("d-none");
})