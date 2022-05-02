import { pizzasAPI } from "../../api/api";

export const setPizzas = (items) => ({ type: "SET_PIZZAS", payload: items });

export const setLoaded = (payload) => ({ type: "SET_LOADED", payload });

// side of thunks

// Получение товаров
export const fetchPizzas = (sortBy, category) => (dispatch) => {
  dispatch(setLoaded(false));
  if (category === null && sortBy === "popular") {
    pizzasAPI.getAllPizzas().then((pizzas) => dispatch(setPizzas(pizzas)));
  } else {
    pizzasAPI
      .getPizzasByParams(sortBy, category)
      .then((pizzas) => dispatch(setPizzas(pizzas)));
  }
};

// Добавление нового товара
export const addNewPizza = (payload) => (dispatch) => {
  dispatch(setLoaded(false));
  pizzasAPI
    .addNewPizza(payload)
    .then((res) =>
      pizzasAPI.getAllPizzas().then((pizzas) => dispatch(setPizzas(pizzas)))
    );
};


// Удаление товара
export const deletePizza = payload => dispatch => {
  dispatch(setLoaded(false));
  pizzasAPI
    .deletePizza(payload)
    .then((res) =>
      pizzasAPI.getAllPizzas().then((pizzas) => dispatch(setPizzas(pizzas)))
    );
}

// Обновление товара
export const updatePizza = payload => dispatch => {
  dispatch(setLoaded(false));
  pizzasAPI
    .updatePizza(payload)
    .then((res) =>
      pizzasAPI.getAllPizzas().then((pizzas) => dispatch(setPizzas(pizzas)))
    );
}