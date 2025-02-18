import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getOrderById, Order } from '../api/order';
import '../styles/components/OrderDetail.css';

const OrderDetail: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [order, setOrder] = useState<Order | null>(null);

    useEffect(() => {
        if (id) {
            getOrderById(Number(id)).then(setOrder);
        }
    }, [id]);

    if (!order) return <p>Loading...</p>;

    return (
        <div key={order.id} className="order-card">
            <div className="order-header">
                <h2>Order # {order.id}</h2>
                <p>Total: <span>${order.totalPrice.toFixed(2)}</span></p>
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
    );
};

export default OrderDetail;
