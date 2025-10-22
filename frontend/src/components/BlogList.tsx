import React from 'react';
import { BlogPost } from '../types';
import BlogCard from './BlogCard';

interface BlogListProps {
  posts: BlogPost[];
}

const BlogList: React.FC<BlogListProps> = ({ posts }) => {
  return (
    <div className="blog-list">
      {posts.map((post) => (
        <BlogCard key={post.id} post={post} />
      ))}
    </div>
  );
};

export default BlogList;
