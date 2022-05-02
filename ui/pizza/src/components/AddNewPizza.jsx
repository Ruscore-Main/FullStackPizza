import React from 'react';
import Button from './Button';
import { Modal } from 'react-bootstrap';
import AddForm from './AddForm';
import { useDispatch } from 'react-redux';

const AddNewPizza = ({dispatch}) => {
  const [show, setShow] = React.useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  return (
    <>
      <Button outline className="button--add" onClick={handleShow}>
        Добавить новый товар
      </Button>

      <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}>
        <Modal.Header closeButton>
          <Modal.Title>Добавление товара</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <AddForm dispatch={dispatch}/>
        </Modal.Body>
      </Modal>
    </>
  );
};

export default AddNewPizza;
