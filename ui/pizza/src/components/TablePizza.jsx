import React from 'react';
import { Table } from 'react-bootstrap';
import { deletePizza } from '../redux/actions/pizzas';
import Button from './Button';
import { Modal } from 'react-bootstrap';
import EditPizzaForm from './EditPizzaForm';


const TablePizza = ({items = [], dispatch}) => {
  
  const [show, setShow] = React.useState(false);
  const [curPizza, setCurPizza] = React.useState(null);

  const handleClose = () => setShow(false);
  const onEditClick = pizza => {
    setCurPizza(pizza);
    setShow(true);
  }

  return (
    <>
    
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
           <td>от {el.price} ₽</td>
           <td>{el.rating}</td>
           <td><Button onClick={() => onEditClick(el)}>Редактировать</Button> <span className='deleteButton' onClick={()=>dispatch(deletePizza(el.id))}>&#10006;</span></td>

         </tr>)
       }
      </tbody>
    </Table>

    <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}>
        <Modal.Header closeButton>
          <Modal.Title>Изменение товара</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <EditPizzaForm pizza={curPizza} dispatch={dispatch}/>
        </Modal.Body>
      </Modal>
    
    </>
  );
};

export default TablePizza;
