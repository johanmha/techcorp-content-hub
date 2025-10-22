import React from 'react';
import { Link } from 'react-router-dom';
import { useCategories } from '../services/api';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const CategoriesListingPage: React.FC = () => {
  const { data: categories, isLoading, error } = useCategories();

  if (isLoading) return <LoadingSpinner />;
  if (error) return <div>Error loading categories: {error.message}</div>;
  if (!categories || categories.length === 0) return <div>No categories found</div>;

  return (
    <div className="categories-listing-page">
      <SEO
        title="Categories - TechCorp Content Hub"
        description="Browse content by category"
      />

      <h1>Categories</h1>
      <p className="page-description">
        Explore our content organized by topics and themes.
      </p>

      <div className="categories-grid">
        {categories.map((category) => (
          <Link
            to={`/category/${category.slug}`}
            key={category.id}
            className="category-card"
            style={{ borderLeftColor: category.color }}
          >
            <h2>{category.name}</h2>
            {category.description && (
              <p className="category-description">{category.description}</p>
            )}
            <span className="view-posts">View Posts →</span>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default CategoriesListingPage;
