import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import OrderList from './components/OrderList';
import ProductList from './components/ProductList';
import OrderDetail from './components/OrderDetail';
import StartPage from './components/StartPage';
import './App.css';

const App = () => {
    return (
        <Router>
            <Switch>
                <Route path="/orders/:id" component={OrderDetail} />
                <Route path="/orders" component={OrderList} />
                <Route path="/products" component={ProductList} />
                <Route path="/" exact component={StartPage}>
                    <h1>Welcome to the Store</h1>
                </Route>
            </Switch>
        </Router>
    );
};

export default App;
