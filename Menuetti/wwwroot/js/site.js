
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

// This adds a new row for the user when adding ingredients to a recipe.

var index = 1; // index for both newIngredient() and deleteRow(id) below.

function setIndex(indx) {
    index = indx;
}

function newIngredient() {

    // new row only if the previous row is not empty
    if (document.getElementById(`ingredient-0-amount`).value != 0 && document.getElementById(`ingredient-0-unit`).value != 0 && document.getElementById(`ingredient-0-name`).value != 0) {

        var olderInputs = "";
        if (document.getElementById("emptyRowError") != null) {
            document.getElementById("emptyRowError").innerHTML = "";
        }

        // Older ingredients - checking the previous inputs using the id number
        for (var i = (index - 1); i >= 0; i--) {

            var previousAmount = document.getElementById(`ingredient-${i}-amount`).value;
            document.getElementById(`ingredient-${i}-amount`).value = "";
            // var previousUnit = document.getElementById(`ingredient-${i}-unit`).value; - not needed atm, unit readonly
            var previousName = document.getElementById(`ingredient-${i}-name`).value;
            document.getElementById(`ingredient-${i}-name`).value = "";

            //console.log("ingredient" + i + " - " + previousAmount + " " + previousName);

            // html for the previous inputs with values
            var previousIngredientRow =
                `<div class="form-group" id="added-item-${i + 1}">
                <div id="ingredient-row">
                    <div class="short-ingredient-box">
                        <input class="form-control" type="number" id=ingredient-${i + 1}-amount min="1" name="Ingredients[${i + 1}].AmountG" required readonly value="${previousAmount}" />
                    </div>
                    <div class="short-ingredient-box">
                        <input class="form-control" type="text" id=ingredient-${i + 1}-unit name="Ingredients[${i + 1}].RecipeUnit" required readonly value="g" />
                    </div>
                    <div class="long-ingredient-box">
                        <input class="form-control" type="text" id=ingredient-${i + 1}-name name="Ingredients[${i + 1}].IngredientName" required readonly value="${previousName}" />
                    </div>
                    <div class="delete-button">
                        <button type="button" onclick="deleteRow(id)" id="delete-button-${i + 1}">X</button>
                    </div>
                </div>
            </div>`

            // adding all the inputs to one block that will be written again
            olderInputs = (olderInputs + previousIngredientRow);

            // when the most previous ingredient is added, printing out
            if (i == 0) {
                document.getElementById("newIngredients").innerHTML = olderInputs;
            }
        }

        index++;
    }
    else {
        var errorMessage = "<div class='text-danger field-validation-error' id='emptyRowError'> Täytä edellinen rivi ensin.</div>"
        document.getElementById("added-item-0").insertAdjacentHTML("afterend", errorMessage);
    }
}

// Deletes the "new ingredient" row when clicked. (Uses the same index as newIngredient()!)
function deleteRow(id) {

    var buttonNro = id.toString().substring(14, 16);
    //console.log("id: " + id + ", buttonNro: " + buttonNro);

    var olderInputs = "";

    // if row clicked is not 0 (= the partial view) changing the id numbers of the input fields
    if (buttonNro != 0) {

        for (var i = index - 1; i >= 0; i--) {

            var previousAmount = document.getElementById(`ingredient-${i}-amount`).value;
            // var previousUnit = document.getElementById(`ingredient-${i}-unit`).value; - not needed atm, unit readonly
            var previousName = document.getElementById(`ingredient-${i}-name`).value;

            if (i < buttonNro && i != 0) {
                // html for the previous inputs with values
                var previousIngredientRow =
                    `<div class="form-group" id="added-item-${i}">
                        <div id="ingredient-row" >
                            <div class="short-ingredient-box">
                                <input class="form-control" type="number" id=ingredient-${i}-amount min="1" name="Ingredients[${i}].AmountG" required readonly value="${previousAmount}" />
                            </div>
                            <div class="short-ingredient-box">
                                <input class="form-control" type="text" id=ingredient-${i}-unit name="Ingredients[${i}].RecipeUnit" required readonly value="g" />
                            </div>
                            <div class="long-ingredient-box">
                                <input class="form-control" type="text" id=ingredient-${i}-name name="Ingredients[${i}].IngredientName" required readonly value="${previousName}" />
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
                        <div id="ingredient-row" >
                            <div class="short-ingredient-box">
                                <input class="form-control" type="number" id=ingredient-${i - 1}-amount min="1" name="Ingredients[${i - 1}].AmountG" required readonly value="${previousAmount}" />
                            </div>
                            <div class="short-ingredient-box">
                                <input class="form-control" type="text" id=ingredient-${i - 1}-unit name="Ingredients[${i - 1}].RecipeUnit" required readonly value="g" />
                            </div>
                            <div class="long-ingredient-box">
                                <input class="form-control" type="text" id=ingredient-${i - 1}-name name="Ingredients[${i - 1}].IngredientName" required readonly value="${previousName}" />
                            </div>
                            <div class="delete-button">
                                <button type="button" onclick="deleteRow(id)" id="delete-button-${i - 1}">X</button>
                            </div>
                        </div>
                     </div>`
            }
            else {
                var previousIngredientRow = "";
                if (i == 0) {
                    index--;
                }
            }

            // adding all the inputs to one block that will be written again
            olderInputs = (olderInputs + previousIngredientRow);

            // when the most previous ingredient is added, printing out
            if (i == 0) {
                document.getElementById("newIngredients").innerHTML = olderInputs;
            }
        }
    }
    // if row clicked is 0 (=the partial view) changing it's value to row-1 + all other input ids--
    else {
        for (var i = index - 1; i >= 0; i--) {

            var previousAmount = document.getElementById(`ingredient-${i}-amount`).value;
            // var previousUnit = document.getElementById(`ingredient-${i}-unit`).value; - not needed atm, unit readonly
            var previousName = document.getElementById(`ingredient-${i}-name`).value;

            if (i != 1) {

                var previousIngredientRow =
                    `<div class="form-group" id="added-item-${i - 1}">
                        <div id="ingredient-row" >
                            <div class="short-ingredient-box">
                                <input class="form-control" type="number" id=ingredient-${i - 1}-amount min="1" name="Ingredients[${i - 1}].AmountG" required readonly value="${previousAmount}" />
                            </div>
                            <div class="short-ingredient-box">
                                <input class="form-control" type="text" id=ingredient-${i - 1}-unit name="Ingredients[${i - 1}].RecipeUnit" required readonly value="g" />
                            </div>
                            <div class="long-ingredient-box">
                                <input class="form-control" type="text" id=ingredient-${i - 1}-name name="Ingredients[${i - 1}].IngredientName" required readonly value="${previousName}" />
                            </div>
                            <div class="delete-button">
                                <button type="button" onclick="deleteRow(id)" id="delete-button-${i - 1}">X</button>
                            </div>
                        </div>
                     </div>`
            }
            if (i == 1) {
                document.getElementById("ingredient-0-amount").value = previousAmount;
                //document.getElementById("ingredient-0-unit").value = previousUnit;
                document.getElementById("ingredient-0-name").value = previousName;
                previousIngredientRow = "";
            }
            if (i == 0) {
                previousIngredientRow = "";
            }

            // adding all the inputs to one block that will be written again
            olderInputs = (olderInputs + previousIngredientRow);

            if (i == 0) {
                document.getElementById("newIngredients").innerHTML = olderInputs;
                index--;

            }

        }
    }
}