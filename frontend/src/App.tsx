import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import OrdersPage from './pages/orders/OrdersPage';
import OrderDetailPage from './pages/orders/OrderDetailPage';
import NewOrderPage from './pages/orders/NewOrderPage';
import ProductsPage from './pages/products/ProductsPage';
import ProductDetailPage from './pages/products/ProductDetailPage';
import NewProductPage from './pages/products/NewProductPage';
import UpdateProductPage from './pages/products/UpdateProductPage';
import NavBar from './components/NavBar';
import './App.css';

const App: React.FC = () => {
    return (
        <Router>
            <NavBar />
            <div className="container">
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/orders" element={<OrdersPage />} />
                    <Route path="/orders/:id" element={<OrderDetailPage />} />
                    <Route path="/orders/new" element={<NewOrderPage />} />
                    <Route path="/products" element={<ProductsPage />} />
                    <Route path="/products/new" element={<NewProductPage />} />
                    <Route path="/products/id/:id" element={<ProductDetailPage />} />
                    <Route path="/products/code/:code" element={<ProductDetailPage />} />
                    <Route path="/products/:id" element={<UpdateProductPage />} />
                </Routes>
            </div>
        </Router>
    );
};

export default App;
