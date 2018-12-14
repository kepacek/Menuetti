// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*setting the previous recipe id to local storage for later shopping list creation and hiding recipe details*/
function fnSaveChoiceBack() {
    localStorage.setItem('recipe0', 0)
    $(".DetCarousel").hide();
}
//hiding by default the recipe details and the toggle function for shoving it
$(document).ready(function () {
    $(".DetCarousel").hide();
});
function showDetCarousel(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel1(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel2(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel3(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel4(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel5(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel6(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel7(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel8(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel9(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel10(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel11(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel12(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel13(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}
function showDetCarousel14(modeli) {
    console.log(modeli)
    $("#" + modeli).toggle();
}

function getRecipesData() {
    var selRecipe0 = localStorage.getItem("recipe0")
    var selRecipe1 = localStorage.getItem("recipe1")
    var selRecipe2 = localStorage.getItem("recipe2")
    var selRecipe3 = localStorage.getItem("recipe3")
    var selRecipe4 = localStorage.getItem("recipe4")

    var selectedRecipes = "../shoppinglist/shoppinglistdetails/"
        + selRecipe0
        + "/" + selRecipe1
        + "/" + selRecipe2
        + "/" + selRecipe3
        + "/" + selRecipe4;


    console.dir(selectedRecipes);
    //fetch(selectedRecipes);
    window.location.href = selectedRecipes;
    //window.location.replace(selectedRecipes);
}
