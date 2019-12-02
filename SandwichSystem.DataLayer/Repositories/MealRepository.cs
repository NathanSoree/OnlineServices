﻿using Microsoft.EntityFrameworkCore;
using SandwichSystem.DataLayer.Extentions;
using SandwichSystem.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using SandwichSystem.Shared.TransfertObjects;
using SandwichSystem.DataLayer.Entities;

namespace SandwichSystem.DataLayer.Repositories
{
    public class MealRepository : IMealRepository
    {
        private MealContext mealContext;

        public MealRepository(MealContext ContextIoC)
        {
            mealContext = ContextIoC ?? throw new ArgumentNullException($"{nameof(ContextIoC)} in MealRepository");
        }

        public bool Remove(MealTO Entity)
            => Remove(Entity.Id);

        public bool Remove(int Id)
        {
            var ReturnValue = false;
            if (!mealContext.Meals.Any(x => x.Id == Id))
                throw new Exception($"MealRepository. Delete(MealId = {Id}) no record to delete.");

            var meal = mealContext.Meals.FirstOrDefault(x => x.Id == Id);
            if (meal != default)
            {
                try
                {
                    mealContext.Meals.Remove(meal);
                    ReturnValue = true;
                }
                catch (Exception)
                {
                    ReturnValue = false;
                }
            }

            return ReturnValue;
        }

        public IEnumerable<MealTO> GetAll()
            => mealContext.Meals
                .AsNoTracking()
                .Include(x => x.MealsComposition)
                    .ThenInclude(x => x.Ingredient)
                .Include(x => x.Supplier)
                .Select(x => x.ToTranfertObject())
                .ToList();

        public MealTO GetByID(int Id)
            => mealContext.Meals
            .AsNoTracking()
            .Include(x => x.MealsComposition)
                .ThenInclude(x => x.Ingredient)
            .Include(x => x.Supplier)
            .FirstOrDefault(x => x.Id == Id).ToTranfertObject();

        public List<MealTO> GetSandwichesByIngredient(List<IngredientTO> Ingredients)
        {
            throw new NotImplementedException();
        }

        public List<MealTO> GetSandwichesBySupplier(SupplierTO Supplier)
            => mealContext.Meals
            .Include(x => x.Supplier)
            .Include(x => x.MealsComposition)
            .Where(x => x.Supplier.Id == Supplier.Id)
            .Select(x => x.ToTranfertObject())
            .ToList();

        public List<MealTO> GetSandwichesWithoutIngredient(List<IngredientTO> Ingredients)
        {
            throw new NotImplementedException();
        }

        public void Insert(MealTO Entity)
        {
            if (!mealContext.Meals.Any(x => x.Id == Entity.Id))
                mealContext.Meals.Add(Entity.ToEF());
        }

        public void Update(MealTO Entity)
        {
            if (!mealContext.Meals.Any(x => x.Id == Entity.Id))
                throw new Exception($"MealRepository. Update(MealTransfertObject) no record to update.");

            var attachedMeal = mealContext.Meals.FirstOrDefault(x => x.Id == Entity.Id);

            if (attachedMeal != default)
                attachedMeal.UpdateFieldsFromDetached(Entity.ToEF());

            mealContext.Meals.Update(attachedMeal);
        }
    }
}

//DOC TO IMPLEMENT? https://www.codeproject.com/Articles/25418/Object-Cloning-Using-Generic-in-C