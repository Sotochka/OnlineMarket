import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../styles/components/OrderForm.css';

interface CreateOrder {
    userId: number;
    customerFullName: string;
    customerPhone: string;
    OrderProducts: { productId: number; amount: number }[];
}

const OrderForm: React.FC = () => {
    const [order, setOrder] = useState<CreateOrder>({
        userId: 0,
        customerFullName: '',
        customerPhone: '',
        OrderProducts: [{ productId: 0, amount: 0 }],
    });

    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setOrder({ ...order, [e.target.name]: e.target.value });
    };

    const addProduct = () => {
        setOrder({
            ...order,
            OrderProducts: [...order.OrderProducts, { productId: 0, amount: 1 }],
        });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        // Simulate order creation
        console.log('Order submitted:', order);
        navigate('/orders');
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Create Order</h2>
            <input
                name="userId"
                placeholder="User Id"
                onChange={handleChange}
                value={order.userId || ''}
                required
            />
            <input
                name="customerFullName"
                placeholder="Customer Name"
                onChange={handleChange}
                value={order.customerFullName}
                required
            />
            <input
                name="customerPhone"
                placeholder="Phone"
                onChange={handleChange}
                value={order.customerPhone}
                required
            />

            {order.OrderProducts.map((p, index) => (
                <div key={index} className="product-group">
                    <input
                        placeholder="Product Id"
                        type="number"
                        value={p.productId}
                        onChange={(e) => {
                            const newProducts = [...order.OrderProducts];
                            newProducts[index].productId = Number(e.target.value);
                            setOrder({ ...order, OrderProducts: newProducts });
                        }}
                        required
                    />
                    <input
                        placeholder="Amount"
                        type="number"
                        value={p.amount}
                        onChange={(e) => {
                            const newProducts = [...order.OrderProducts];
                            newProducts[index].amount = Number(e.target.value);
                            setOrder({ ...order, OrderProducts: newProducts });
                        }}
                        required
                    />
                </div>
            ))}

            <button type="button" onClick={addProduct}>➕ Add Product</button>
            <button type="submit">✅ Submit Order</button>
        </form>
    );
};

export default OrderForm;
