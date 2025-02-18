import React from 'react';
import ProductList from '../../components/ProductList';
import { Link } from 'react-router-dom';
import '../../styles/components/ProductList.css';

const ProductsPage: React.FC = () =>(
    <div>
        <h2 style={{marginBottom: '20px'}}>Products</h2>
        <Link to="/products/new" className='create-product-btn'>Create Product</Link>
        <ProductList />
    </div>
);

export default ProductsPage;
