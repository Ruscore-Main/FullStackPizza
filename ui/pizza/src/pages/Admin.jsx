import { AddNewPizza, TablePizza } from './../components';
import React from 'react';
import { fetchPizzas } from '../redux/actions/pizzas';
import { useDispatch, useSelector } from 'react-redux';

const Admin = () => {

  const {items, isLoaded} = useSelector(({ pizzas }) => ({
    items: pizzas.items,
    isLoaded: pizzas.isLoaded
  }));

  const dispatch = useDispatch();

  React.useEffect(() => {
    dispatch(fetchPizzas('popular', null));
  }, []);



  return (
    <div className="container">
      <h2 className="content__title">Панель администрации</h2>
      <div className="admin__wrapper">
        <AddNewPizza dispatch={dispatch}/>
        
        {
          isLoaded ?
          <TablePizza items={items} dispatch={dispatch}/>
          : "Идет загрузка"
        }
      </div>
    </div>
  );
};

export default Admin;
