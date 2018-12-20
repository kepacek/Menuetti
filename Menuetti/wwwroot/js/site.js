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

// Toggle functions for the carousel (shows the recipe details)
function showTheCarousel(modeli) {
    $("#recipe-" + modeli).toggle();
}
function closeTheCarousels() {
    var selectedRecipes = document.getElementsByClassName("item active");
    for (let i = 0; i < selectedRecipes.length; i++) {
        $("#recipe-" + selectedRecipes[i].id).hide();
    }
}

// This checks which recipes were chosen (active) from the carousel and send them and the user to the shopping list view
function sendToShoppinglist() {
    var selectedRecipes = document.getElementsByClassName("item active");

    let recipeIdsListed = [];
    for (let i = 0; i < selectedRecipes.length; i++) {
        recipeIdsListed.push(selectedRecipes[i].id)
    }
    let redirectUrlWithParameters = "/shoppinglist/recipes/";
    for (let i = 0; i < recipeIdsListed.length; i++) {
        redirectUrlWithParameters += (recipeIdsListed[i].toString() + "/");
    }
    window.location.href = redirectUrlWithParameters;
}

// This adds new row for the user when adding ingredients to a recipe.
var index = 0; // index for both newIngredient() and deleteRow(id) below.
function newIngredient() {
    var olderInputs = "";

    // html for an empty input row
    var newEmptyIngredientRow =
        `<div class="form-group" id="added-item-${i}">
                <div class="row" id="ingredient-row" >
                    <div class="short-ingredient-box">
                        <input class="form-control" type="number" id=incredient-${index}-amount min="1" name="Ingredients[${index}].AmountG" />
                    </div>
                    <div class="short-ingredient-box">
                        <input class="form-control" type="text" id=incredient-${index}-unit name="Ingredients[${index}].RecipeUnit" readonly value="g" />
                    </div>
                    <div class="long-ingredient-box">
                        <input class="form-control" type="text" id=incredient-${index}-name name="Ingredients[${index}].IngredientName" />
                    </div>
                    <div class="delete-button">
                        <button type="button" onclick="deleteRow(id)" id="delete-button-${index}">X</button>
                    </div>
                </div>
             </div>`

    // Adding the first ingredient (input row) - no need to check the previous inputs
    if (index == 0) {
        document.getElementById("newIngredients").innerHTML += (newEmptyIngredientRow += document.getElementById("newIngredients").innerHTML);
    }
    else {
        // Other ingredients - checking the previous inputs using id number
        for (var i = 0; i < index; i++) {

            var previousAmount = document.getElementById(`incredient-${i}-amount`).value;
            // var previousUnit = document.getElementById(`incredient-${i}-unit`).value; - not needed atm, unit readonly
            var previousName = document.getElementById(`incredient-${i}-name`).value;

            // html for the previous inputs with values
            var previousIngredientRow =
                `<div class="form-group" id="added-item-${i}">
                        <div class="row" id="ingredient-row" >
                            <div class="short-ingredient-box">
                                <input class="form-control" type="number" id=incredient-${i}-amount min="1" name="Ingredients[${i}].AmountG" value="${previousAmount}" />
                            </div>
                            <div class="short-ingredient-box">
                                <input class="form-control" type="text" id=incredient-${i}-unit name="Ingredients[${i}].RecipeUnit" readonly value="g" />
                            </div>
                            <div class="long-ingredient-box">
                                <input class="form-control" type="text" id=incredient-${i}-name name="Ingredients[${i}].IngredientName" value="${previousName}" />
                            </div>
                            <div class="delete-button">
                                <button type="button" onclick="deleteRow(id)" id="delete-button-${i}">X</button>
                            </div>
                        </div>
                     </div>`

            // adding all the inputs to one block that will be written again
            olderInputs = (olderInputs + previousIngredientRow);

            // when the most previous ingredient is added, printing out
            if (i == index - 1) {
                document.getElementById("newIngredients").innerHTML = olderInputs;
                var oldIngredientsAndEmpty = (olderInputs += newEmptyIngredientRow);
                document.getElementById("newIngredients").innerHTML = oldIngredientsAndEmpty;
            }
        }
    }
    index++;
}

// Deletes the "new ingredient" row when clicked. (Uses the same index as newIngredient()!)
function deleteRow(id) {

    var buttonNro = id.toString().substring(14, 16);
    console.log("id: " + id + ", buttonNro: " + buttonNro);

    var olderInputs = "";

    for (var i = 0; i <= index; i++) {

        var previousAmount = document.getElementById(`incredient-${i}-amount`).value;
        // var previousUnit = document.getElementById(`incredient-${i}-unit`).value; - not needed atm, unit readonly
        var previousName = document.getElementById(`incredient-${i}-name`).value;

        if (i < buttonNro) {
            // html for the previous inputs with values
            var previousIngredientRow =
                `<div class="form-group" id="added-item-${i}">
                        <div class="row" id="ingredient-row" >
                            <div class="short-ingredient-box">
                                <input class="form-control" type="number" id=incredient-${i}-amount min="1" name="Ingredients[${i}].AmountG" value="${previousAmount}" />
                            </div>
                            <div class="short-ingredient-box">
                                <input class="form-control" type="text" id=incredient-${i}-unit name="Ingredients[${i}].RecipeUnit" readonly value="g" />
                            </div>
                            <div class="long-ingredient-box">
                                <input class="form-control" type="text" id=incredient-${i}-name name="Ingredients[${i}].IngredientName" value="${previousName}" />
                            </div>
                            <div class="delete-button">
                                <button type="button" onclick="deleteRow(id)" id="delete-button-${i}">X</button>
                            </div>
                        </div>
                      </div>`
        }
        else if (i > buttonNro) {
            // html for the previous inputs with values
            var previousIngredientRow =
                `<div class="form-group" id="added-item-${i - 1}">
                        <div class="row" id="ingredient-row" >
                            <div class="short-ingredient-box">
                                <input class="form-control" type="number" id=incredient-${i - 1}-amount min="1" name="Ingredients[${i - 1}].AmountG" value="${previousAmount}" />
                            </div>
                            <div class="short-ingredient-box">
                                <input class="form-control" type="text" id=incredient-${i - 1}-unit name="Ingredients[${i - 1}].RecipeUnit" readonly value="g" />
                            </div>
                            <div class="long-ingredient-box">
                                <input class="form-control" type="text" id=incredient-${i - 1}-name name="Ingredients[${i - 1}].IngredientName" value="${previousName}" />
                            </div>
                            <div class="delete-button">
                                <button type="button" onclick="deleteRow(id)" id="delete-button-${i - 1}">X</button>
                            </div>
                        </div>
                     </div>`
        }
        else {
            var previousIngredientRow = "";
            index--;
        }

        console.log("index: " + index);
        console.log("i: " + i);
        console.log(previousIngredientRow);

        // adding all the inputs to one block that will be written again
        olderInputs = (olderInputs + previousIngredientRow);
        console.log("old inputs:")
        console.log(olderInputs)

        // when the most previous ingredient is added, printing out
        if (i == index) {
            document.getElementById("newIngredients").innerHTML = olderInputs;

        }
    }
}