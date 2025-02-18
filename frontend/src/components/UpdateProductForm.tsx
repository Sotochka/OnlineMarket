import React, { useState } from 'react';
import { UpdateProduct, updateProduct } from '../api/product';
import { useNavigate, useParams } from 'react-router-dom';

const ProductForm: React.FC = () => {
    const [product, setProduct] = useState<UpdateProduct>({
        code: '',
        name: '',
        price: 0,
    });
    
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setProduct({ ...product, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await updateProduct(product, Number(id));
        navigate('/products');
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Update Product</h2>
            <input name="code" placeholder="Code" onChange={handleChange} required />
            <input name="name" placeholder="Name" onChange={handleChange} required />
            <input name="price" placeholder="Price" type="number" onChange={handleChange} required />
            <button type="submit">Update Product</button>
        </form>
    );
};

export default ProductForm;
