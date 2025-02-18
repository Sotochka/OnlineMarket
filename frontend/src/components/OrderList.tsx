import React, { useEffect, useState } from 'react';
import { getOrders, Order } from '../api/order';
import { Link } from 'react-router-dom';
import '../styles/components/OrderList.css';

const OrderList: React.FC = () => {
    const [orders, setOrders] = useState<Order[]>([]);

    useEffect(() => {
        getOrders().then(setOrders);
    }, []);

    return (
        <div className='orders-container'>
            <div className="orders-grid">
                {orders.map((order) => (
                    <div key={order.id} className="order-card">
                        <div className="order-header">
                            <div>
                                <h2>Order # {order.id}</h2>
                                <p>Total: <span>${order.totalPrice.toFixed(2)}</span></p>
                            </div>
                            <Link to={`/orders/${order.id}`} className='view-order-btn'>View Order</Link>
                        </div>

                        <div className="order-details">
                            <p><strong>Name:</strong> {order.customerFullName}</p>
                            <p><strong>Phone:</strong> {order.customerPhone}</p>
                        </div>

                        <div className="products-list">
                            <h3>Products:</h3>
                            <div className="products-grid">
                                {order.products.map((product) => (
                                    <div key={product.productId} className="product-item">
                                        <span><strong>ID:</strong> {product.productId}</span>
                                        <span><strong>Name:</strong> {product.productName}</span>
                                        <span><strong>Amount:</strong> {product.productAmount}</span>
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default OrderList;
