import React from 'react';
import { Author } from '../types';
import { Link } from 'react-router-dom';

interface AuthorBioProps {
  author: Author;
}

const AuthorBio: React.FC<AuthorBioProps> = ({ author }) => {
  return (
    <div className="author-bio">
      {author.profileImage && <img src={author.profileImage} alt={author.name} />}
      <h3><Link to={`/author/${author.id}`}>{author.name}</Link></h3>
      <p>{author.title}</p>
      <p>{author.bio}</p>
      {/* Add social links */}
    </div>
  );
};

export default AuthorBio;
