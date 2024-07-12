import { message, Form, Dropdown } from 'antd';
import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import moment from 'moment';
import numeral from 'numeral';
import { WarehouseService, ProductStallService } from '../api';
import ModalEditProduct from '../components/modals/edit-product-stall-modal';
import { emptyArray, renderColorStatus } from '../core/utils';
import useKeyPress from '../hook/Click';
import threeDot from '../assets/images/three-dot.svg';

import { useUI } from '../hook/useUI';
import { EnumStatus, PRODUCT_PLATFORM } from '../core/constants';

const productsStallService = new ProductStallService();

const cols = [
  { name: 'Tên', className: 'px-6 py-3', style: {} },
  { name: 'Trạng thái', className: 'px-6 py-3', style: {} },
  { name: 'Số lượng', className: 'px-6 py-3', style: {} },
  { name: 'Phần trăm', className: 'px-6 py-3', style: {} },
  { name: 'Giá bán', className: 'px-6 py-3', style: {} },
  { name: 'Phụ phí', className: 'px-6 py-3', style: {} },
  { name: 'Nền tảng', className: 'px-6 py-3', style: {} },
  { name: 'Tên gian hàng', className: 'px-6 py-3', style: {} },
  { name: 'Hành động', className: 'px-6 py-3', style: {} },
];

const items = [
  {
    label: <span className="px-4 py-2">Chỉnh sửa</span>,
    key: '1',
  },
];

function ProductStall() {
  const onEnter = useKeyPress('Enter');
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPage] = useState(1);
  const [product, setProduct] = useState([]);
  const { setLoading, loading } = useUI();

  const [modalEditProduct, setModalEditProduct] = useState(false);
  const detail = useRef({});
  const [filterData, setFilterData] = useState({});
  const [form] = Form.useForm();

  const navigate = useNavigate();

  const mappingUsersTable = useCallback((product) => {
    return product.map((user) => ({ ...user, check: false }));
  }, []);

  const [searchName, setSearchName] = useState('');

  const fetchData = async (dataFilter = {}) => {
    try {
      const limit = 12;
      setLoading(true);
      const response = await productsStallService.actGetProducts({
        limit,
        skip: page - 1,
        // keyword: searchName,
        // role: 'user',
        // createdAtSort: 'desc',
        ...dataFilter,
      });
      setProduct(mappingUsersTable(response.data.data) || []);
      setTotalPage(Math.ceil(response.data.total / limit));
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    fetchData(filterData);
  }, [page]);

  const onClickAction = ({ key }, product) => {
    switch (key) {
      case '1': {
        detail.current = product;
        setModalEditProduct(true);
      }
    }
  };
  const renderPages = () => {
    return (
      <li>
        <button
          type="button"
          className="py-2 px-3 leading-tight  bg-black text-white rounded-md">
          {page}
        </button>
      </li>
    );
  };

  const renderRows = () => {
    if (emptyArray(product)) return null;
    return product.map((product) => (
      <tr
        key={product._id}
        className="bg-white even:bg-zinc-50 even:bg-opacity- border-b"
        // onClick={() => navigate(`/warehouses/${product._id.toString()}`)}
      >
        {/* <th scope="row" className="px-6 py-4 font-medium text-gray-900 ">
            <div className="flex items-center">
              <input
                onChange={(e) => {
                  updateItemUser(userId, { check: e.target.checked });
                }}
                id="checkbox"
                type="checkbox"
                value={check}
                className="form-checkbox  w-4 h-4 text-black bg-gray-100 rounded border-gray-300 focus:ring-black"
              />
            </div>
          </th> */}
        <td className="px-6 py-2">{product.productName || '\u2014'}</td>
        <td
          className={`px-6 py-2 font-extrabold ${renderColorStatus(
            product.status
          )}`}>
          {EnumStatus[product.status]}
        </td>
        <td className="px-6 py-2">{numeral(product.qty).format('0,0')}</td>

        {/* New */}
        <td className="px-6 py-2">{numeral(0).format('%')}</td>

        <td className="px-6 py-2">{numeral(product.inventory?.price).format('0,0')}</td>
        <td className="px-6 py-2">{numeral(product.fee).format('0,0')}</td>
        <td className="px-6 py-2">{PRODUCT_PLATFORM[product.platform]}</td>
        <td className="px-6 py-2">{product.stallName || '\u2014'}</td>
        <td className="px-6 py-2">
          <Dropdown
            menu={{
              items,
              onClick: (e) => onClickAction(e, product),
            }}
            placement="topRight">
            <span className="border-[1px] border-black rounded-full p-1 cursor-pointer hover:scale-125">
              <img
                src={threeDot}
                alt="dot"
                style={{ display: 'initial', aspectRatio: 1 }}
              />
            </span>
          </Dropdown>
        </td>
      </tr>
    ));
  };

  // useEffect(() => {
  //   if (onEnter && !loading && searchName) {
  //     request();
  //   }
  // }, [onEnter]);

  const filter = (data) => {
    const keys = Object.keys(data);
    const formatedData = {};
    for (const key of keys) {
      const value = data[key];
      if (value) formatedData[key] = value;
    }
    if (formatedData.fromDate) {
      formatedData.fromDate = moment(formatedData.fromDate).format(
        'DD/MM/YYYY'
      );
    }
    if (formatedData.toDate) {
      formatedData.toDate = moment(formatedData.toDate).format('DD/MM/YYYY');
    }
    setFilterData(formatedData);
    return fetchData(formatedData);
  };

  return (
    <>
      {modalEditProduct && (
        <ModalEditProduct
          opened={modalEditProduct}
          setOpened={setModalEditProduct}
          onSuccess={() => {
            fetchData();
          }}
          detail={detail.current}
        />
      )}
      <div
        style={{ minHeight: 'calc(100% - 200px)' }}
        className="bg-white mt-[24px] mr-[22px] rounded-[12px]">
        <div className="px-[24px] py-[15px]">
          <div
            style={{ height: 'calc(100vh - 210px)' }}
            className="mt-[30px] relative overflow-x-scroll  shadow-md sm:rounded-lg">
            <table className="w-full text-xs text-left text-gray-500">
              <thead className="text-xs text-gray-700 uppercase bg-gray-50 sticky top-0 z-10">
                <tr>
                  {cols.map((col, idx) => (
                    <th
                      scope="col"
                      className={col.className}
                      key={idx}>
                      {col.name}
                    </th>
                  ))}
                </tr>
              </thead>
              <tbody>{renderRows()}</tbody>
            </table>
          </div>
        </div>
      </div>

      <nav aria-label="">
        <ul className="flex justify-end mr-[22px] mt-[16px] gap-[8px] items-center">
          {page !== 1 ? (
            <li>
              <button
                onClick={() => setPage(page - 1)}
                type="button"
                className="py-2 px-3 ml-0 leading-tight text-black bg-white rounded-md font-bold">
                {'<'}
              </button>
            </li>
          ) : null}

          {renderPages()}
          <span className="text-black">{`of ${totalPages}`}</span>
          {totalPages && page !== totalPages ? (
            <li>
              <button
                onClick={() => setPage(page + 1)}
                type="button"
                className="py-2 px-3 leading-tight text-black bg-white rounded-md font-bold">
                {'>'}
              </button>
            </li>
          ) : null}
        </ul>
      </nav>
    </>
  );
}

export default ProductStall;
