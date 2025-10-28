﻿using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.Drinks;

namespace ShopTARge24.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkServices _drinkServices;

        public DrinksController
            (
                IDrinkServices drinkServices
            )
        {
            _drinkServices = drinkServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchDrink(DrinkSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Drink", "Drinks", new { drink = model.Drink });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Drink(string drink)
        {
            var dto = new DrinkDto { StrDrink = drink };

            await _drinkServices.DrinkResponseDto(dto);

            var vm = new DrinkViewModel
            {
                Drink = dto.StrDrink ?? drink,
                IdDrink = dto.IdDrink,
                StrAlcoholic = dto.StrAlcoholic,
                StrGlass = dto.StrGlass,
                StrInstructions = dto.StrInstructions,
                StrDrinkThumb = dto.StrDrinkThumb,
                StrIngredient1 = dto.StrIngredient1,
                StrIngredient2 = dto.StrIngredient2,
                StrIngredient3 = dto.StrIngredient3,
                StrIngredient4 = dto.StrIngredient4,
                StrIngredient5 = dto.StrIngredient5,
                StrIngredient6 = dto.StrIngredient6,
                StrIngredient7 = dto.StrIngredient7,
                StrIngredient8 = dto.StrIngredient8,
                StrIngredient9 = dto.StrIngredient9,
                StrIngredient10 = dto.StrIngredient10,
                StrIngredient11 = dto.StrIngredient11,
                StrIngredient12 = dto.StrIngredient12,
                StrIngredient13 = dto.StrIngredient13,
                StrIngredient14 = dto.StrIngredient14,
                StrIngredient15 = dto.StrIngredient15,
                StrMeasure1 = dto.StrMeasure1,
                StrMeasure2 = dto.StrMeasure2,
                StrMeasure3 = dto.StrMeasure3,
                StrMeasure4 = dto.StrMeasure4,
                StrMeasure5 = dto.StrMeasure5,
                StrMeasure6 = dto.StrMeasure6,
                StrMeasure7 = dto.StrMeasure7,
                StrMeasure8 = dto.StrMeasure8,
                StrMeasure9 = dto.StrMeasure9,
                StrMeasure10 = dto.StrMeasure10,
                StrMeasure11 = dto.StrMeasure11,
                StrMeasure12 = dto.StrMeasure12,
                StrMeasure13 = dto.StrMeasure13,
                StrMeasure14 = dto.StrMeasure14,
                StrMeasure15 = dto.StrMeasure15
            };

            return View(vm);
        }
    }
}