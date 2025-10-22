import React from 'react';
import { useParams } from 'react-router-dom';
import { useCategory } from '../services/api';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const CategoryPage: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const { data: category, isLoading, error } = useCategory(slug || '');

  if (!slug) {
    return <p className="error-message">Category slug is missing.</p>;
  }

  if (isLoading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">Failed to fetch category details.</p>;
  }

  if (!category) {
    return <p>Category not found.</p>;
  }

  return (
    <div>
      <SEO title={category.name} description={category.description} />
      <h1 style={{ color: category.color }}>Category: {category.name}</h1>
      <p>{category.description}</p>
      {/* Potentially list blog posts in this category */}
    </div>
  );
};

export default CategoryPage;
