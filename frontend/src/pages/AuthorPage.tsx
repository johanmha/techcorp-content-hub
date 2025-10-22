import React from 'react';
import { useParams } from 'react-router-dom';
import { useAuthor } from '../services/api';
import AuthorBio from '../components/AuthorBio';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const AuthorPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const { data: author, isLoading, error } = useAuthor(id || '');

  if (!id) {
    return <p className="error-message">Author ID is missing.</p>;
  }

  if (isLoading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">Failed to fetch author details.</p>;
  }

  if (!author) {
    return <p>Author not found.</p>;
  }

  return (
    <div>
      <SEO title={author.name} description={author.bio} image={author.profileImage} />
      <h1>Author: {author.name}</h1>
      <AuthorBio author={author} />
      {/* Potentially list blog posts by this author */}
    </div>
  );
};

export default AuthorPage;
