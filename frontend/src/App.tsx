import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Layout from './components/Layout';
import HomePage from './pages/HomePage';
import BlogListingPage from './pages/BlogListingPage';
import BlogDetailPage from './pages/BlogDetailPage';
import AuthorPage from './pages/AuthorPage';
import CategoryPage from './pages/CategoryPage';
import NotFoundPage from './pages/NotFoundPage';

// Create a client
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      retry: 1,
      staleTime: 5 * 60 * 1000, // 5 minutes
    },
  },
});

const App: React.FC = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <Layout>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/blog" element={<BlogListingPage />} />
            <Route path="/blog/:slug" element={<BlogDetailPage />} />
            <Route path="/author/:id" element={<AuthorPage />} />
            <Route path="/category/:slug" element={<CategoryPage />} />
            <Route path="*" element={<NotFoundPage />} /> {/* Catch-all for 404 */}
          </Routes>
        </Layout>
      </Router>
    </QueryClientProvider>
  );
};

export default App;
