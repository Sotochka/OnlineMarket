import React, { useState } from 'react';
import { createProduct, Product } from '../api/product';
import { useNavigate } from 'react-router-dom';

const ProductForm: React.FC = () => {
    const [product, setProduct] = useState<Product>({
        code: '',
        name: '',
        price: 0,
    });
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setProduct({ ...product, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await createProduct(product);
        navigate('/products');
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Create Product</h2>
            <input name="code" placeholder="Code" onChange={handleChange} required />
            <input name="name" placeholder="Name" onChange={handleChange} required />
            <input name="price" placeholder="Price" type="number" onChange={handleChange} required />
            <button type="submit">Save Product</button>
        </form>
    );
};

export default ProductForm;
