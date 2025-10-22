import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Author } from '../types';
import { getAuthorById } from '../services/api';
import AuthorBio from '../components/AuthorBio';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const AuthorPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [author, setAuthor] = useState<Author | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!id) {
      setError('Author ID is missing.');
      setLoading(false);
      return;
    }

    const fetchAuthor = async () => {
      try {
        const data = await getAuthorById(id);
        setAuthor(data);
      } catch (err) {
        setError('Failed to fetch author details.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchAuthor();
  }, [id]);

  if (loading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">{error}</p>;
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
