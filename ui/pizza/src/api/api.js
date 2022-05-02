import axios from "axios";

const BASE_URL = "http://localhost:50111/api/pizza";

export const instance = axios.create({
  baseURL: BASE_URL,
});

export const pizzasAPI = {
  // Возвращает массив всех пицц
  getAllPizzas() {
    return instance.get().then((resp) => resp.data);
  },
  // Возвращает массив пицц по параметрам
  getPizzasByParams(sortBy, category) {
    return instance.get(`?${category !== null ? `category=${category}&` : ''}_sort=${sortBy}&_order=asc`).then((resp) => resp.data);
  },

  addNewPizza(pizza) {
    return instance.post('', pizza)
  },

  deletePizza(id) {
    return instance.delete(`/${id}`)
  },

  updatePizza(pizza) {
    console.log(pizza);
    return instance.put(`/${pizza.id}`, pizza)
  }

};
