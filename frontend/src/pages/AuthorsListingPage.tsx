import React from 'react';
import { Link } from 'react-router-dom';
import { useAuthors } from '../services/api';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const AuthorsListingPage: React.FC = () => {
  const { data: authors, isLoading, error } = useAuthors();

  if (isLoading) return <LoadingSpinner />;
  if (error) return <div>Error loading authors: {error.message}</div>;
  if (!authors || authors.length === 0) return <div>No authors found</div>;

  return (
    <div className="authors-listing-page">
      <SEO
        title="Authors - TechCorp Content Hub"
        description="Meet our team of expert authors and contributors"
      />

      <h1>Our Authors</h1>
      <p className="page-description">
        Meet the talented writers and experts behind our content.
      </p>

      <div className="authors-grid">
        {authors.map((author) => (
          <Link
            to={`/author/${author.id}`}
            key={author.id}
            className="author-card"
          >
            {author.profileImage && (
              <img
                src={author.profileImage}
                alt={author.name}
                className="author-image"
              />
            )}
            <div className="author-info">
              <h2>{author.name}</h2>
              {author.title && <p className="author-title">{author.title}</p>}
              {author.bio && (
                <p className="author-bio-excerpt">
                  {author.bio.length > 150
                    ? `${author.bio.substring(0, 150)}...`
                    : author.bio}
                </p>
              )}
            </div>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default AuthorsListingPage;
