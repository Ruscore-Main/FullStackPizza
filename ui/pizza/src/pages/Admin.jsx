import React from 'react'

const Admin = () => {
    return (
        <div className="container">
          <div className="content__top">
            <Categories
              onClickItem={onSelectCategory}
              items={categoryNames}
              activeCategory={category}
            />
            <SortPopup items={sortTypes} currentSort={sortBy} />
          </div>
          <h2 className="content__title">Все пиццы</h2>
          <div className="content__items">
            {isLoaded
              ? pizzas.map((pizza) => <PizzaCard onClickAddPizza={handleAddPizzaToCart} {...pizza} key={pizza.id} />)
              : Array.from(Array(10), (_, index) => <LoaderPizzaBlock key={index} />)}
          </div>
        </div>
      );
}

export default Admin