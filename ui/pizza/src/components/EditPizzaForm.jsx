import React from "react";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Button from "react-bootstrap/Button";
import FormControl from "react-bootstrap/FormControl";
import PizzaCard from "./PizzaCard";
import { Button as SaveButton } from "../components";
import { updatePizza } from "../redux/actions/pizzas";

class EditPizzaForm extends React.Component {
  // Первоначальная инициализация
  constructor(props) {
    super(props);

    const { id, name, rating, imageUrls, sizes, types, price } = props.pizza;

    this.state = {
      id,
      name,
      rating,
      types,
      price,

      images: {
        26: "",
        30: "",
        40: "",
      },

      have26: sizes.includes(26),
      have30: sizes.includes(30),
      have40: sizes.includes(40),
    };

    sizes.forEach((el, i) => {
      this.state.images[el] = imageUrls[i];
    });

    console.log(this.state);
  }

  componentDidUpdate() {
    console.log('Компонент обновлен - ', this.state);
  }

  onNameChange = (e) => {
    this.setState({ name: e.target.value });
  };

  onPriceChange = (e) => {
    this.setState({ price: e.target.value });
  };

  onRatingChange = (e) => {
    this.setState({ rating: e.target.value });
  };

  haveImageBtnClick = (size) => {
    const key = `have${size}`;

    this.setState((prev) => {
      let hasSize = prev[key];
      if (hasSize) {
        return { [key]: !hasSize, images: { ...prev.images, [size]: "" } };
      }
      return { [key]: !hasSize };
    });
  };

  onTypeChange = (e) => {
    const value = +e.target.value;
    this.setState((prev) => {
      let types = [...prev.types];
      if (e.target.checked) {
        types.push(value);
      } else {
        types = types.filter((el) => el != value);
      }

      return { types };
    });
  };

  onImageUrlsChange = (e, size) => {
    this.setState((prev) => ({
      images: {
        ...prev.images,
        [size]: e.target.value,
      },
    }));
  };

  getPizzaState = () => {
    let { name, price, rating, types, images, id } = this.state;
    let previewState = {
      id,
      name,
      price,
      rating,
      types,
      imageUrls: [],
      sizes: [],
    };

    for (let keyValues of Object.entries(images)) {
      const [key, value] = keyValues;

      if (value != "") {
        previewState.sizes.push(+key);
        previewState.imageUrls.push(value);
      }
    }
    return previewState;
  };

  validateState = () => {
    const { name, price, sizes, imageUrls, types } = this.getPizzaState();
    if (
      !name ||
      !price ||
      !sizes.length ||
      !imageUrls.length ||
      !types.length
    ) {
      return false;
    }
    return true;
  };

  savePizza = (e) => {
    e.preventDefault();
    if (this.validateState()) {
      this.props.dispatch(updatePizza(this.getPizzaState()));
    } else {
      alert("НЕ ВСЕ ПОЛЯ ЗАПОЛНЕНЫ!");
    }
  };

  render() {
    return (
      <>
        <Form onSubmit={this.savePizza}>
          <Form.Group className="mb-3">
            <Form.Label>Название</Form.Label>
            <Form.Control
              placeholder="Введите название"
              value={this.state.name}
              onChange={this.onNameChange}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Цена</Form.Label>
            <Form.Control
              placeholder="Цена ₽"
              value={this.state.price}
              onChange={this.onPriceChange}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Рейтинг: {this.state.rating}</Form.Label>
            <Form.Range
              min={1}
              max={10}
              value={this.state.rating}
              onChange={this.onRatingChange}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Размер</Form.Label>
            {[26, 30, 40].map((el, i) => (
              <InputGroup className="mb-3">
                <Button
                  variant="outline-secondary"
                  onClick={() => this.haveImageBtnClick(el)}
                >
                  {el} см.
                </Button>
                {this.state[`have${el}`] ? (
                  <FormControl
                    placeholder="URL изображения"
                    value={this.state.images[el]}
                    onChange={(e) => this.onImageUrlsChange(e, el)}
                  />
                ) : (
                  <FormControl
                    placeholder="URL изображения"
                    value={this.state.images[el]}
                    disabled
                  />
                )}
              </InputGroup>
            ))}
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Тип</Form.Label>
            <div>
              <Form.Check
                inline
                checked={this.state.types.includes(0)}
                label="Тонкое"
                value="0"
                type="checkbox"
                onChange={this.onTypeChange}
              />
              <Form.Check
                inline
                checked={this.state.types.includes(1)}
                label="Традиционное"
                value="1"
                type="checkbox"
                onChange={this.onTypeChange}
              />
            </div>
          </Form.Group>
          <div className="d-flex justify-content-center mb-3">
            <SaveButton>Сохранить</SaveButton>
          </div>
        </Form>
        <div className="d-flex align-items-center flex-column">
          <h2 className="mb-3">Превью</h2>
          <PizzaCard {...this.getPizzaState()} isPreview />
        </div>
      </>
    );
  }
}
export default EditPizzaForm;
