"use strict";

let dropdown = document.querySelector(".home1");
let openedList = document.querySelector(".od1");

let categoryDropdown = document.querySelector(".c1");
let openedCategories = document.querySelector(".category1");

let isDropdownOpen = false;
let isCategoryDropdownOpen = false;

function openDropdown1() {
    openedList.classList.add("active-dropdown");
    isDropdownOpen = true;
}

function closeDropdown1() {
    if (!isDropdownOpen) {
        openedList.classList.remove("active-dropdown");
    }
}

function openCategoryDropdown1() {
    openedCategories.classList.add("active-category");
    isCategoryDropdownOpen = true;
}

function closeCategoryDropdown1() {
    if (!isCategoryDropdownOpen) {
        openedCategories.classList.remove("active-category");
    }
}

dropdown.addEventListener("mouseover", openDropdown1);
dropdown.addEventListener("mouseout", closeDropdown1);
openedList.addEventListener("mouseover", function() {
    isDropdownOpen = true;
});
openedList.addEventListener("mouseout", function() {
    isDropdownOpen = false;
});

categoryDropdown.addEventListener("mouseover", openCategoryDropdown1);
categoryDropdown.addEventListener("mouseout", closeCategoryDropdown1);
openedCategories.addEventListener("mouseover", function() {
    isCategoryDropdownOpen = true;
});
openedCategories.addEventListener("mouseout", function() {
    isCategoryDropdownOpen = false;
});

document.addEventListener("mouseover", function() {
    if (!isDropdownOpen && !isCategoryDropdownOpen) {
        closeDropdown1();
        closeCategoryDropdown1();
    }
});

let dropdown2 = document.querySelector(".home2");
let openedList2 = document.querySelector(".od2");

let categoryDropdown2 = document.querySelector(".c2");
let openedCategories2 = document.querySelector(".category2");

isDropdownOpen = false;
isCategoryDropdownOpen = false;

function openDropdown2() {
    openedList2.classList.add("active-dropdown");
    isDropdownOpen = true;
}

function closeDropdown2() {
    if (!isDropdownOpen) {
        openedList2.classList.remove("active-dropdown");
    }
}

function openCategoryDropdown2() {
    openedCategories2.classList.add("active-category");
    isCategoryDropdownOpen = true;
}

function closeCategoryDropdown2() {
    if (!isCategoryDropdownOpen) {
        openedCategories2.classList.remove("active-category");
    }
}

dropdown2.addEventListener("mouseover", openDropdown2);
dropdown2.addEventListener("mouseout", closeDropdown2);
openedList2.addEventListener("mouseover", function() {
    isDropdownOpen = true;
});
openedList2.addEventListener("mouseout", function() {
    isDropdownOpen = false;
});

categoryDropdown2.addEventListener("mouseover", openCategoryDropdown2);
categoryDropdown2.addEventListener("mouseout", closeCategoryDropdown2);
openedCategories2.addEventListener("mouseover", function() {
    isCategoryDropdownOpen = true;
});
openedCategories2.addEventListener("mouseout", function() {
    isCategoryDropdownOpen = false;
});

document.addEventListener("mouseover", function() {
    if (!isDropdownOpen && !isCategoryDropdownOpen) {
        closeDropdown2();
        closeCategoryDropdown2();
    }
});

const navbar = document.querySelector(".navbar");
const navbar2 = document.querySelector(".second-nav");
let startPosition = navbar.offsetTop;
function stickynavbar() {
  if (window.scrollY > startPosition) {    
    navbar.classList.add('d-none');
    navbar2.classList.add('fixed-top');
    navbar2.classList.remove('d-none');
  } else {
    navbar.classList.remove('d-none');   
    navbar2.classList.remove('fixed-top');
    navbar2.classList.add('d-none'); 
  }
}
window.addEventListener('scroll', stickynavbar);

let pages = document.querySelectorAll(".pagination .num");

pages.forEach(page => {
    page.addEventListener("click", function(){
        pages.forEach(p => p.classList.remove("active"));
        this.classList.add("active");
    })
});
