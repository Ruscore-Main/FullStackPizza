import "./index.scss";

import { Home, Basket, Admin } from "./pages";
import { Route, Link   } from "react-router-dom";

import Header from "./components/Header";
import React from "react";

const App = () => {

  return (
    <div className="wrapper">
      <Header />
      <div className="content">
        <Route exact path="/" render={() => <Home />} />
        <Route exact path="/basket" render={() => <Basket />} />
        <Route exact path="/admin" render={() => <Admin />} />
      </div>
      <Route exact path="/" render={()=><div className="adminLink"><Link to={'/admin'}>Admin</Link></div>} />
    </div>
  );
};

export default App;
