import React from 'react';
import { Table } from 'react-bootstrap';
import Button from './Button';

const TablePizza = ({items = []}) => {
  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>#</th>
          <th>Название</th>
          <th>Изображения</th>
          <th>Цена</th>
          <th>Рейтинг</th>
          <th></th>

        </tr>
      </thead>
      <tbody>
       {
         items.map(el => <tr key={el.id}>
           <td>{el.id}</td>
           <td>{el.name}</td>
           <td className="tdImage">{el.imageUrls.map((el, i) => <img className='tableImage' src={el} key={`${i}_${el}`}/>)}</td>
           <td>{el.price}</td>
           <td>{el.rating}</td>
           <td><Button>Редактировать</Button></td>

         </tr>)
       }
      </tbody>
    </Table>
  );
};

export default TablePizza;
