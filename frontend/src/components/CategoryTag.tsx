import React from 'react';
import { Category } from '../types';
import { Link } from 'react-router-dom';

interface CategoryTagProps {
  category: Category;
}

const CategoryTag: React.FC<CategoryTagProps> = ({ category }) => {
  return (
    <Link to={`/category/${category.slug}`} className="category-tag" style={{ backgroundColor: category.color || '#ccc' }}>
      {category.name}
    </Link>
  );
};

export default CategoryTag;
