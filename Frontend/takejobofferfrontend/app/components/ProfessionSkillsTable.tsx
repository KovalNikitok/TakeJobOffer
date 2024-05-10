import { Table } from 'antd';
import { AlignType } from 'rc-table/lib/interface';
import { useEffect, useState } from 'react';

interface Props {
  values: ProfessionSkillWithName[];
}

interface ProfessionSkillColumnProps {
  name: string;
  mentionCount: number;
}

export const ProfessionSkillsTable = ({ values }: Props) => {
    const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
    const [columnsData, setColunmsData] = useState<ProfessionSkillColumnProps[]>([]);
    useEffect(() => {
      setProfessionSkills(values);
      let columns: ProfessionSkillColumnProps[] = [];
      professionSkills.map((professionSkill) => {

        let col: ProfessionSkillColumnProps = {name: professionSkill.name, mentionCount: professionSkill.mentionCount};
        columns.push(col);
      });

      setColunmsData(columns);
    }, [values]);

    const columns = [
      {
        title: 'Навык',
        dataIndex: 'name',
        width: 250,
      },
      {
        title: 'Упоминаний',
        dataIndex: 'mentionCount',
        align: 'center' as AlignType,
        width: 30,
        sorter: (firstSkill: ProfessionSkillColumnProps, secondSkill: ProfessionSkillColumnProps) => firstSkill.mentionCount - secondSkill.mentionCount,
      },
    ];
    
    return (
      <Table 
        columns={columns} 
        dataSource={columnsData} 
        pagination={{ pageSize: 10 }}
      />
    );
}